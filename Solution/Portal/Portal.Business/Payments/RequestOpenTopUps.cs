using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portal.Business
{
    public class RequestOpenTopUps
    {
        private readonly GetOpenTopUps _getOpenTopUps;

        public RequestOpenTopUps(GetOpenTopUps getOpenTopUps)
        {
            _getOpenTopUps = getOpenTopUps;
        }

        public async Task<IEnumerable<Transaction>> RequestAllOpenTopUps()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Transaction, Models.Transaction>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<IEnumerable<Transaction>>(_getOpenTopUps.GetAllOpenTransactions());
        }

        public async Task<IEnumerable<Transaction>> RequestOpenTopUpsFromEmployee(string email)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Transaction, Models.Transaction>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<IEnumerable<Transaction>>(_getOpenTopUps.GetAllOpenTransactionsFromEmployee(email));
        }
    }
}