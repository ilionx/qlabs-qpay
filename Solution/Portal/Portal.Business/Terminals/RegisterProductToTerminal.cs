using Portal.DataAccess;
using Portal.DataAccess.Models;
using System.Threading.Tasks;

namespace Portal.Business
{
    public class RegisterProductToTerminal
    {
        private readonly EditTerminalProduct _editTerminalProduct;

        public RegisterProductToTerminal(EditTerminalProduct editTerminalProduct)
        {
            _editTerminalProduct = editTerminalProduct;
        }

        public async Task RegisterProductToTerminalWithProductId(string terminalId, string terminalDescription, int productId)
        {
            Terminal terminal = new Terminal
            {
                TerminalId = terminalId,
                TerminalDescription = terminalDescription,
                ProductId = productId
            };
            await _editTerminalProduct.EditTerminal(terminal);
        }
    }
}