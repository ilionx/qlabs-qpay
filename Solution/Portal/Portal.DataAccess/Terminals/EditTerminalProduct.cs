using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System;
using System.Threading.Tasks;

namespace Portal.DataAccess
{
    public class EditTerminalProduct
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public EditTerminalProduct(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<EditTerminalProduct>();
            _context = context;
        }

        public async Task EditTerminal(Terminal terminal)
        {
            try
            {
                _context.Update(terminal);
                await _context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
        }
    }
}