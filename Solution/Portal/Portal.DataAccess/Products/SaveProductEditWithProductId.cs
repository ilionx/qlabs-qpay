using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;

namespace Portal.DataAccess
{
    public class SaveProductEditWithProductId
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public SaveProductEditWithProductId(PortalContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<SaveCardToUser>();
        }

        public async void SaveProductEdit(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}