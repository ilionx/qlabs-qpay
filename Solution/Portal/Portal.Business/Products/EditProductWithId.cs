using AutoMapper;
using Portal.DataAccess;
using System.Threading.Tasks;

namespace Portal.Business
{
    public class EditProductWithId
    {
        private readonly SaveProductEditWithProductId _saveEmployeeEditWithCardId;

        public EditProductWithId(SaveProductEditWithProductId saveEmployeeEditWithCardId)
        {
            _saveEmployeeEditWithCardId = saveEmployeeEditWithCardId;
        }

        public async Task EditProduct(Models.Product product)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Models.Product, DataAccess.Models.Product>());

            var mapper = mapperConfig.CreateMapper();

            _saveEmployeeEditWithCardId.SaveProductEdit(mapper.Map<DataAccess.Models.Product>(product));
        }
    }
}