using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Portal.DataAccess
{
    public class GetAllProducts
    {
        private readonly ILogger _logger;
        private readonly PortalContext _portalContext;

        public GetAllProducts(ILoggerFactory loggerFactory, PortalContext portalContext)
        {
            _logger = loggerFactory.CreateLogger<GetAllProducts>();
            _portalContext = portalContext;
        }

        public IEnumerable<Product> GetProducts()
        {
            var query = _portalContext.Products.ToList();

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