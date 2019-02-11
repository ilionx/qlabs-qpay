using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;

namespace Portal.DataAccess
{
    public class SaveEmployeeEditWithCardId
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public SaveEmployeeEditWithCardId(PortalContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<SaveCardToUser>();
        }

        public async void SaveEmployeeEdits(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}