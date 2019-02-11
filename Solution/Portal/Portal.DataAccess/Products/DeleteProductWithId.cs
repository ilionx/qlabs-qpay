using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.DataAccess
{
    public class DeleteProductWithId
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public DeleteProductWithId(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<DeleteProductWithId>();
            _context = context;
        }

        public async Task<Product> GetProduct(int id)
        {
            var selectedProduct = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
            try
            {
                if (selectedProduct != null)
                {
                    return selectedProduct;
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
            return new Product();
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var selectedProduct = _context.Products.AsNoTracking()
                    .Where(product => product.ProductId == id);
                Product remProduct = new Product
                {
                    ProductId = selectedProduct.First().ProductId
                };

                var terminals = _context.Terminals
                    .Where(b => b.ProductId == id)
                    .ToList();
                
                foreach(Terminal terminal in terminals)
                {
                    terminal.ProductId = null;
                    _context.Terminals.Update(terminal);
                }

                await _context.SaveChangesAsync();

                _context.Products.Remove(remProduct);
                await _context.SaveChangesAsync();
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
        }
    }
}