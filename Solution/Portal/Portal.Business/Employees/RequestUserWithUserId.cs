using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;

namespace Portal.Business
{
    public class RequestUserWithUserId
    {
        private readonly GetUserWithUserId _getUserWithUserId;

        public RequestUserWithUserId(GetUserWithUserId getUserWithUserId)
        {
            _getUserWithUserId = getUserWithUserId;
        }

        public Employee RequestEmployee(string employeeId)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Employee, Employee>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<Employee>(_getUserWithUserId.GetEmployee(employeeId));
        }
    }
}