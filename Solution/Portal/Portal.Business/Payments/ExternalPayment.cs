using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Portal.Interfaces;

namespace Portal.Business
{
    public class ExternalPayment
    {
        private readonly INewExternalPayment _newExternalPayment;

        public ExternalPayment(INewExternalPayment newExternalPayment)
        {
            _newExternalPayment = newExternalPayment;
        }

        public async Task<string> SelectPaymentProvider(string provider, string amount)
        {
            if (provider == "PayPal")
            {
                return _newExternalPayment.CreateNewPayment(amount).Result;
            }

            return "Failed";
        }

        public async Task<string> ExecutePayPalPayment(string paymentId, string token, string payerId, string employeeMail)
        {
            if (_newExternalPayment.ExecutePayment(paymentId, token, payerId, employeeMail).Result == "Complete")
            {
                return "ExternalPaymentComplete";
            }

            return "Failed";
        }
    }
}
