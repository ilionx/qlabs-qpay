using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Portal.DataAccess
{
    public class GetAllTransactions
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public GetAllTransactions(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<GetAllUsers>();
            _context = context;
        }

        public IEnumerable<Transaction> GetEveryTransaction()
        {
            var query = _context.Transactions.Include(t => t.Employee);

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

        public IEnumerable<Transaction> GetIncomeOverview()
        {
            return _context.Transactions.Where(x => x.TransactionType == "CLOSED");
        }

        public IEnumerable<Transaction> GetOpenTotalTransactions()
        {
            return _context.Transactions.Where(x => x.TransactionType == "OPEN");
        }
    }
}