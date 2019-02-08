using System;
using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;

namespace Portal.Business
{
    public class RequestProductWithProductId
    {
        private readonly GetProductWithProductId _getProductWithProductId;

        public RequestProductWithProductId(GetProductWithProductId getProductWithProductId)
        {
            _getProductWithProductId = getProductWithProductId;
        }

        public Product RequestProduct(string productId)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Product, Product>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<Product>(_getProductWithProductId.GetProduct(Int32.Parse(productId)));
        }
    }
}