using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Portal.DataAccess
{
    public class GetAllUsers
    {
        public readonly ILogger _logger;
        public readonly PortalContext _context;

        public GetAllUsers(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<GetAllUsers>();
            _context = context;
        }

        public IEnumerable<Employee> GetEveryUser()
        {
            var query = _context.Employees.ToList();

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