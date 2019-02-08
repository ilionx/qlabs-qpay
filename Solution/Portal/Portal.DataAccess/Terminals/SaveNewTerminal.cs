using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Portal.DataAccess
{
    public class SaveNewTerminal
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public SaveNewTerminal(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<SaveNewTerminal>();
            _context = context;
        }

        public async Task CreateNewTerminal(Terminal terminal)
        {
            try
            {
                _context.Add(terminal);
                await _context.SaveChangesAsync();
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
        }
    }
}