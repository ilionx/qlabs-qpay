using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using Portal.Interfaces;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Portal.DataAccess
{
    public class SaveOfflineTopUp : IOfflineTopUp
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public SaveOfflineTopUp(PortalContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<SaveOfflineTopUp>();
        }

        public async Task<bool> SaveOfflineTopUpTransaction(string employeeEmail, decimal topUpAmount)
        {
            var employee = _context.Employees.Find(employeeEmail);
            employee.Balance = employee.Balance + topUpAmount;

            var newTransaction = new Transaction
            {
                DateTime = DateTime.UtcNow,
                Amount = topUpAmount,
                TransactionType = "OPEN",
                Employee = employee
            };

            try
            {
                _context.Add(newTransaction);
                _context.Update(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
                return false;
            }
        }
    }
}