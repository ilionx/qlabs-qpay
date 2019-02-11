using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Portal.DataAccess
{
    public class GetProductWithProductId
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public GetProductWithProductId(PortalContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<GetUserWithCardId>();
        }

        public Product GetProduct(int productId)
        {
            var query = _context.Products
                .Where(product => product.ProductId == productId);

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
            return new Product();
        }
    }
}