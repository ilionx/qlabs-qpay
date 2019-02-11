using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Linq;

namespace Portal.DataAccess
{
    public class GetUserWithCardId
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public GetUserWithCardId(PortalContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<GetUserWithCardId>();
        }

        public bool GetCardInfo(string cardId)
        {
            var query = _context.Employees
                .Where(employee => employee.CardUid == cardId);

            try
            {
                if (query.Any()) //If there is a record in the query this means the card is already/still attached to a user
                {
                    return false;
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
            return true;
        }
    }
}