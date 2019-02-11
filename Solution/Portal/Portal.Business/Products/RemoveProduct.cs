using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;
using System.Threading.Tasks;

namespace Portal.Business
{
    public class RemoveProduct
    {
        private readonly DeleteProductWithId _deleteProductWithId;

        public RemoveProduct(DeleteProductWithId deleteProductWithId)
        {
            _deleteProductWithId = deleteProductWithId;
        }

        public async Task<Product> checkIfProductIsValid(int id)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Product, Product>());
            var mapper = mapperConfig.CreateMapper();
            return mapper.Map<Product>(await _deleteProductWithId.GetProduct(id));
        }

        public async Task RemoveProductWithId(int id)
        {
            await _deleteProductWithId.DeleteProduct(id);
        }
    }
}