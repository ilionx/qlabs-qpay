using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;
using System.Collections.Generic;

namespace Portal.Business
{
    public class RequestAllUsers
    {
        private readonly GetAllUsers _getAllUsers;

        public RequestAllUsers(GetAllUsers getAllUsers)
        {
            _getAllUsers = getAllUsers;
        }

        public IEnumerable<Employee> RequestEveryUser()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Employee, Employee>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<IEnumerable<Employee>>(_getAllUsers.GetEveryUser());
        }
    }
}