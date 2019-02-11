using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Portal.DataAccess
{
    public class GetOpenTopUps
    {
        private readonly ILogger _logger;
        private readonly PortalContext _portalContext;

        public GetOpenTopUps(ILoggerFactory loggerFactory, PortalContext portalContext)
        {
            _logger = loggerFactory.CreateLogger<GetAllProducts>();
            _portalContext = portalContext;
        }

        public IEnumerable<Transaction> GetAllOpenTransactions()
        {
            var result = _portalContext.Transactions
                .Where(transaction => transaction.TransactionType == "OPEN")
                .ToList();
            try
            {
                if (result.Any())
                {
                    return result;
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
            return null;
        }

        public IEnumerable<Transaction> GetAllOpenTransactionsFromEmployee(string employeeEmail)
        {
            var employee = _portalContext.Employees.Find(employeeEmail);

            var result = _portalContext.Transactions
                .Where(transaction => transaction.TransactionType == "OPEN")
                .Where(transaction => transaction.Employee == employee)
                .ToList();
            try
            {
                if (result.Any())
                {
                    return result;
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
            return null;
        }
    }
}