using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Linq;

namespace Portal.DataAccess
{
    public class GetLoggedInUserInfo
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public GetLoggedInUserInfo(PortalContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<GetLoggedInUserInfo>();
        }

        public struct GetUserInfoResult
        {
            public bool IsUserRegistered { get; set; }
            public decimal Balance { get; set; }
            public string CardId { get; set; }
            public bool Admin { get; set; }
            public int AmountOpenTransactions { get; set; }
        }

        public GetUserInfoResult GetUserInfo(string userEmail)
        {
            var query = _context.Employees
                .Where(employee => employee.Email == userEmail);

            try
            {
                if (query.Any())
                {
                    return new GetUserInfoResult
                    {
                        IsUserRegistered = true,
                        Balance = query.First().Balance,
                        CardId = query.First().CardUid,
                        Admin = query.First().Admin,
                        AmountOpenTransactions = GetOpenTransactions(userEmail)
                    };
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
            return new GetUserInfoResult();
        }

        public int GetOpenTransactions(string userEmail)
        {
            var query = _context.Transactions
                .Where(transaction => transaction.Employee.Email == userEmail && transaction.TransactionType == "OPEN");
            try
            {
                if (query.Any())
                {
                    
                    return query.Count();
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("WHile running this error showed up", error);
            }
            return query.Count();
        }
    }
}