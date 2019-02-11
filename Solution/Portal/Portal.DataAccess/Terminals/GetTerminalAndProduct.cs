using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Portal.DataAccess
{
    public class GetTerminalAndProduct
    {
        public readonly ILogger _logger;
        public readonly PortalContext _context;

        public GetTerminalAndProduct(ILoggerFactory loggerFactory, PortalContext context)
        {
            _logger = loggerFactory.CreateLogger<GetTerminalAndProduct>();
            _context = context;
        }

        public IEnumerable<TEMPTerminalAndProduct> GetAllTerminalsWithProducts()
        {
            var query = from terminal in _context.Terminals.AsNoTracking()
                        join product in _context.Products on terminal.ProductId equals product.ProductId into temp
                        from product in temp.DefaultIfEmpty()
                        select new TEMPTerminalAndProduct
                        {
                            TerminalId = terminal.TerminalId,
                            TerminalDescription = terminal.TerminalDescription,
                            ProductId = terminal.Product.ProductId,
                            Productname = terminal.Product.Productname,
                            ProductDescription = terminal.Product.ProductDescription,
                            ProductPrice = terminal.Product.ProductPrice
                        };
            try
            {
                if (query.Any())
                {
                    return query;
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
            }
            return query;
        }
    }
}