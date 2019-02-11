using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;
using System.Threading.Tasks;

namespace Portal.Business
{
    public class RegisterNewProduct
    {
        private readonly SaveNewProduct _saveNewProduct;

        public RegisterNewProduct(SaveNewProduct saveNewProduct)
        {
            _saveNewProduct = saveNewProduct;
        }

        public async Task RegisterProduct(Product product)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Models.Product, DataAccess.Models.Product>());

            var mapper = mapperConfig.CreateMapper();

            await _saveNewProduct.CreateNewProduct(mapper.Map<DataAccess.Models.Product>(product));
        }
    }
}