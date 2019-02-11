using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.DataAccess
{
    public class DeleteTerminalWithId
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public DeleteTerminalWithId(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<DeleteTerminalWithId>();
            _context = context;
        }

        public async Task<Terminal> GetTerminal(string id)
        {
            var selectedTerminal = await _context.Terminals.AsNoTracking().SingleOrDefaultAsync(m => m.TerminalId == id);
            try
            {
                if (selectedTerminal != null)
                {
                    return selectedTerminal;
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
            return new Terminal();
        }

        public async Task DeleteTerminal(string id)
        {
            try
            {
                var selectedTerminal = _context.Terminals.AsNoTracking()
                    .Where(terminal => terminal.TerminalId == id);

                Terminal remTerminal = new Terminal
                {
                    TerminalId = selectedTerminal.First().TerminalId
                };

                _context.Terminals.Remove(remTerminal);
                await _context.SaveChangesAsync();
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
        }
    }
}