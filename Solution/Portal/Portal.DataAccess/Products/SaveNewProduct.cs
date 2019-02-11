using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Portal.DataAccess
{
    public class SaveNewProduct
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public SaveNewProduct(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<SaveNewProduct>();
            _context = context;
        }

        public async Task CreateNewProduct(Product product)
        {
            try
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
        }
    }
}