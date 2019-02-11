using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Portal.DataAccess
{
    public class GetNewCards
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public GetNewCards(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<GetNewCards>();
            _context = context;
        }

        public IEnumerable<NewCards> GetAllNewCards()
        {
            var query = _context.NewCards.ToList();

            try
            {
                if (query.Any())
                {
                    return query;
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("WHile running this error showed up", error);
            }
            return query;
        }
    }
}