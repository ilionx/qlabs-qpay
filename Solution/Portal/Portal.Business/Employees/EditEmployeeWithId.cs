using AutoMapper;
using Portal.DataAccess;
using System.Threading.Tasks;

namespace Portal.Business
{
    public class EditEmployeeWithId
    {
        private readonly SaveEmployeeEditWithCardId _saveEmployeeEditWithCardId;

        public EditEmployeeWithId(SaveEmployeeEditWithCardId saveEmployeeEditWithCardId)
        {
            _saveEmployeeEditWithCardId = saveEmployeeEditWithCardId;
        }

        public async Task EditEmployee(Models.Employee employee)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Models.Employee, DataAccess.Models.Employee>());

            var mapper = mapperConfig.CreateMapper();

            var mappedEmployee = mapper.Map<DataAccess.Models.Employee>(employee);

            _saveEmployeeEditWithCardId.SaveEmployeeEdits(mappedEmployee);
        }
    }
}