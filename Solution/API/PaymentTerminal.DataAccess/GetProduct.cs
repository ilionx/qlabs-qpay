using System.Linq;

namespace PaymentTerminal.DataAccess
{
    public class GetProduct
    {
        private readonly PaymentTerminalContext _context;

        public GetProduct(PaymentTerminalContext context)
        {
            _context = context;
        }

        public struct GetProductPriceResult
        {
            public bool ValidTerminal { get; set; }
            public decimal ProductPrice { get; set; }
            public int ProductId { get; set; }
        }

        public GetProductPriceResult GetProductPriceFromTerminal(string deviceUid)
        {
            var query = from terminal in _context.Terminals
                join product in _context.Products
                    on terminal.ProductId equals product.ProductId
                where terminal.TerminalId == deviceUid
                select product;

            if (query.Any())
            {
                return new GetProductPriceResult
                {
                    ProductPrice = query.First().ProductPrice,
                    ProductId = query.First().ProductId,
                    ValidTerminal = true
                };
            }
            return new GetProductPriceResult();
        }

        public string GetProductNameFromTerminal(string deviceUid)
        {
            var query = from terminal in _context.Terminals
                join product in _context.Products
                    on terminal.ProductId equals product.ProductId
                where terminal.TerminalId == deviceUid
                select product;

            if (query.Any())
            {
                return query.First().Productname;
            }

            return "NoProductAttached";
        }
    }
}