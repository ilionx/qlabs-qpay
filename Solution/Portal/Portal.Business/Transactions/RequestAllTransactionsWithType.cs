using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;
using System.Collections.Generic;

namespace Portal.Business
{
    public class RequestAllTransactionsWithType
    {
        private readonly GetAllTransactionsWithType _getAllOpenTransactions;

        public RequestAllTransactionsWithType(GetAllTransactionsWithType getAllOpenTransactions)
        {
            _getAllOpenTransactions = getAllOpenTransactions;
        }

        public IEnumerable<Transaction> RequestEveryTransaction(string transactionType)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Transaction, Models.Transaction>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<IEnumerable<Transaction>>(_getAllOpenTransactions.GetOpenTransactions(transactionType));
        }
    }
}