using Microsoft.Extensions.Logging;
using PaymentTerminal.DataAccess.Models;
using PaymentTerminal.Interfaces;
using System;

namespace PaymentTerminal.DataAccess
{
    public class WriteTransaction : IWriteTransaction
    {
        private readonly PaymentTerminalContext _context;
        private readonly ILogger _logger;

        public WriteTransaction(PaymentTerminalContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<WriteTransaction>();
        }

        public bool SaveTransaction(string employeeEmail, string transactionType, decimal transactionAmount, decimal newBalance, int productId)
        {
            var employee = _context.Employees.Find(employeeEmail);
            employee.Balance = newBalance;
            _context.Update(employee);
            var transaction = new Transaction
            {
                DateTime = DateTime.UtcNow,
                Amount = transactionAmount,
                ProductId = productId,
                TransactionType = transactionType,
                Employee = employee
            };

            try
            {
                _context.Add(transaction);

                _context.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
                return false;
            }
        }
    }
}