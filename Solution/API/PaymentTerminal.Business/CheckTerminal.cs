using Microsoft.Extensions.Logging;
using PaymentTerminal.DataAccess;
using PaymentTerminal.Interfaces;

namespace PaymentTerminal.Business
{
    public class CheckTerminal : ICheckTerminal
    {
        private readonly GetProduct _getProductPrice;
        private readonly ILogger _logger;

        public CheckTerminal(GetProduct getProductPrice, ILoggerFactory loggerFactory)
        {
            _getProductPrice = getProductPrice;
            _logger = loggerFactory.CreateLogger<CheckTerminal>();
        }

        public (decimal ProductPrice, bool TerminalFound, int ProductId) ValidateTerminal(string deviceUid)
        {
            decimal productPrice;
            bool productFound;
            int productId;

            //Get product price from database
            try
            {
                var result = _getProductPrice.GetProductPriceFromTerminal(deviceUid);
                productPrice = result.ProductPrice;
                productId = result.ProductId;
                productFound = result.ValidTerminal;
            }
            catch (System.Data.SqlClient.SqlException error)
            {
                //Log error with app insights
                _logger.LogError("While running this error showed up:", error);
                return (0, false, 0);
            }
            return (productPrice, productFound, productId);
        }
    }
}