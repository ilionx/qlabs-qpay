using System;
using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;
using System.Collections.Generic;

namespace Portal.Business
{
    public class RequestAllTransactions
    {
        private readonly GetAllTransactions _getAllTransactions;

        public RequestAllTransactions(GetAllTransactions getAllTransactions)
        {
            _getAllTransactions = getAllTransactions;
        }

        public IEnumerable<Transaction> RequestEveryTransaction()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Transaction, Models.Transaction>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<IEnumerable<Transaction>>(_getAllTransactions.GetEveryTransaction());
        }

        public decimal RequestIncomeOverviewMonth()
        {
            decimal totalIncome = 0;
            foreach (var transaction in _getAllTransactions.GetIncomeOverview())
            {
                DateTime transactionDateTime = transaction.DateTime;
                if (transactionDateTime.Month == DateTime.UtcNow.Month &&
                    transactionDateTime.Year == DateTime.UtcNow.Year)
                {
                    totalIncome = totalIncome + transaction.Amount;
                }
            }
            return totalIncome;
        }

        public decimal RequestTotalOpenTransactions()
        {
            decimal totalOpen = 0;
            foreach (var transaction in _getAllTransactions.GetOpenTotalTransactions())
            {
                totalOpen = totalOpen + transaction.Amount;
            }
            return totalOpen;
        }

        public decimal RequestIncomeOverviewTotal()
        {
            decimal totalIncome = 0;
            foreach (var transaction in _getAllTransactions.GetIncomeOverview())
            {
                    totalIncome = totalIncome + transaction.Amount;
            }
            return totalIncome;
        }
    }
}