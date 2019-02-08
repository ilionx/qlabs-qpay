using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Portal.Interfaces;
using BraintreeHttp;
using Microsoft.Extensions.Logging;
using PayPal.Core;
using PayPal.v1.Payments;

namespace Portal.DataAccess
{
    public class PayPal : INewExternalPayment
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public PayPal(ILoggerFactory loggerFactory, PortalContext context)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<PayPal>();
        }

        public async Task<string> CreateNewPayment(string amount)
        {
            var environment = new SandboxEnvironment("AV8B5CBqWFqiUEBdhbe2rBVqX0RwNjY74nORYSEI3P8WZ-rRzRxXd1H0pA_qrywn0MCZuXzg3x-WwDeY", "EL-1QQduQKljgE0uHFs6WKAM4JtYrbf8CzOisxNCczPvdnkQYQDT1-rCM1W1dYM3Dhu8L9AcwqwhKdZH");
            var client = new PayPalHttpClient(environment);
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = amount,
                            Currency = "EUR",
                            Details = new AmountDetails()
                            {
                                Tax = "0",
                                Subtotal = amount
                            }
                        },
                        ItemList = new ItemList()
                        {
                            Items = new List<Item>()
                            {
                                new Item()
                                {
                                    Name="QPay Saldo",
                                    Currency = "EUR",
                                    Price = amount,
                                    Quantity = "1",
                                    Sku = "Sku",
                                    Tax  = "0",
                                    Description = "QPay Saldo"
                                }
                            }

                        }, Description = "Opwaarderen QPay",
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = "https://pilotqpayportal.azurewebsites.net/Payment/Failed",
                    ReturnUrl = "https://pilotqpayportal.azurewebsites.net/Payment/Execute" //RELEASE RETURN URL
                    //ReturnUrl = "https://localhost:44354/Payment/Execute" //Localhost return URL
                },
                
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                },
                NoteToPayer = amount + "euro Tegoed"
            };

            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                HttpResponse response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();
                LinkDescriptionObject approvalLink = findApprovalLink(result.Links);
                string redirectUrl = approvalLink.Href;
                return redirectUrl;
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                return "Failed";
            }
        }

        private static LinkDescriptionObject findApprovalLink(List<LinkDescriptionObject> links)
        {
            foreach (var link in links)
            {
                if (link.Rel.Equals("approval_url"))
                {
                    return link;
                }
            }
            return null;
        }

        public async Task<string> ExecutePayment(string paymentId, string token, string payerId,string email)
        {
            var environment = new SandboxEnvironment("AV8B5CBqWFqiUEBdhbe2rBVqX0RwNjY74nORYSEI3P8WZ-rRzRxXd1H0pA_qrywn0MCZuXzg3x-WwDeY", "EL-1QQduQKljgE0uHFs6WKAM4JtYrbf8CzOisxNCczPvdnkQYQDT1-rCM1W1dYM3Dhu8L9AcwqwhKdZH");
            var client = new PayPalHttpClient(environment);
            PaymentExecuteRequest request = new PaymentExecuteRequest(paymentId);
            request.RequestBody(new PaymentExecution()
            {
                PayerId = payerId
            });


            try
            {
                HttpResponse response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                //PayerId en payment opslaan in DB
                var employee = _context.Employees.Find(email);
                employee.Balance = employee.Balance + 10;
                Models.Transaction transaction = new Models.Transaction()
                {
                    Amount = 10,
                    DateTime = DateTime.UtcNow,
                    Employee = employee,
                    employeeEmail = email,
                    ProviderName = "PayPal",
                    ProviderTransactionId = paymentId,
                    TransactionType = "ExternalTopUp"
                };
                try
                {
                    _context.Add(transaction);
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    return "Complete";
                }
                catch (SqlException error)
                {
                    _logger.LogError("While running this error showed up:", error);
                    return "Failed";
                }
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                return "Failed";
            }
        }
    }
}
