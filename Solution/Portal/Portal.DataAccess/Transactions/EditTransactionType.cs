using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System;
using System.Threading.Tasks;

namespace Portal.DataAccess
{
    public class EditTransactionType
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public EditTransactionType(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<EditTransactionType>();
            _context = context;
        }

        public async Task EditTransaction(int transactionId, string newType)
        {
            try
            {
                var transaction = _context.Find<Transaction>(transactionId);
                transaction.TransactionType = newType;
                _context.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
        }
    }
}