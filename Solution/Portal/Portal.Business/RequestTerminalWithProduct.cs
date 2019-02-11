using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;
using Portal.DataAccess.Models;
using System.Collections.Generic;

namespace Portal.Business
{
    public class RequestTerminalWithProduct
    {
        private readonly GetTerminalAndProduct _getTerminalAndProduct;

        public RequestTerminalWithProduct(GetTerminalAndProduct getTerminalAndProduct)
        {
            _getTerminalAndProduct = getTerminalAndProduct;
        }

        public IEnumerable<TerminalAndProduct> RequestAllTerminalsWithProduct()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<TEMPTerminalAndProduct, TerminalAndProduct>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<IEnumerable<TerminalAndProduct>>(_getTerminalAndProduct.GetAllTerminalsWithProducts());
        }
    }
}