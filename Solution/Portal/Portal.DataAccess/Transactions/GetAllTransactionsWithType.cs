using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Portal.DataAccess
{
    public class GetAllTransactionsWithType
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public GetAllTransactionsWithType(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<GetAllUsers>();
            _context = context;
        }

        public IEnumerable<Transaction> GetOpenTransactions(string transactionType)
        {
            var query = _context.Transactions.Include(t => t.Employee)
                .Where(b => b.TransactionType == transactionType);

            try
            {
                if (query.Any())
                {
                    return query;
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
            return query;
        }
    }
}