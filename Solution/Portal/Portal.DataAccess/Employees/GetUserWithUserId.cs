using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Data.SqlClient;
using System.Linq;

namespace Portal.DataAccess
{
    public class GetUserWithUserId
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public GetUserWithUserId(PortalContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<GetUserWithCardId>();
        }

        public Employee GetEmployee(string userId)
        {
            var query = _context.Employees
                .Where(employee => employee.Email == userId);
            try
            {
                if (query.Any())
                {
                    return query.First();
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
            return new Employee();
        }
    }
}