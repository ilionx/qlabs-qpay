using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;
using System.Collections.Generic;

namespace Portal.Business
{
    public class RequestAllProducts
    {
        public readonly GetAllProducts _getAllProducts;

        public RequestAllProducts(GetAllProducts getAllProducts)
        {
            _getAllProducts = getAllProducts;
        }

        public IEnumerable<Product> RequestEveryProduct()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Product, TerminalAndProduct>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<IEnumerable<Product>>(_getAllProducts.GetProducts());
        }
    }
}