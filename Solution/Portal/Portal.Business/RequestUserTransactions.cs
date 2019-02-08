using AutoMapper;
using Portal.DataAccess;
using System.Collections.Generic;
using Transaction = Portal.Business.Models.Transaction;

namespace Portal.Business
{
    public class RequestUserTransactions
    {
        private readonly GetUsersTransactions _getUsersTransactions;

        public RequestUserTransactions(GetUsersTransactions getUsersTransactions)
        {
            _getUsersTransactions = getUsersTransactions;
        }

        public IEnumerable<Transaction> RequestTransactions(string userId)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Transaction, Models.Transaction>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<IEnumerable<Transaction>>(_getUsersTransactions.GetTransactionsWithUserId(userId));
        }
    }
}