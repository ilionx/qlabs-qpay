using Microsoft.Extensions.Logging;
using Portal.DataAccess;
using Portal.Interfaces;
using System.Data.SqlClient;
using BraintreeHttp;

namespace Portal.Business
{
    public class RequestUserInfoFromDataprovider : IRequestUserInfoFromDataprovider
    {
        private readonly GetLoggedInUserInfo _getLoggedInUserInfo;
        private readonly ILogger _logger;

        public RequestUserInfoFromDataprovider(GetLoggedInUserInfo getLoggedInUserInfo, ILoggerFactory loggerFactory)
        {
            _getLoggedInUserInfo = getLoggedInUserInfo;
            _logger = loggerFactory.CreateLogger<RequestUserInfoFromDataprovider>();
        }

        public (bool IsUserRegistered, decimal Balance, string CardId, int AccountLevel,int AmountTransactions) RequestUserData(string loggedInUserEmail)
        {
            bool isUserRegistered;
            decimal balance;
            string cardId;
            int accountLevel;
            int amountTransactions;

            try
            {
                var result = _getLoggedInUserInfo.GetUserInfo(loggedInUserEmail);
                isUserRegistered = result.IsUserRegistered;
                balance = result.Balance;
                cardId = result.CardId;
                amountTransactions = result.AmountOpenTransactions;

                if (isUserRegistered && result.Admin)
                {
                    accountLevel = 2;
                }
                else
                {
                    if (isUserRegistered == false)
                    {
                        accountLevel = 0;
                    }
                    else
                    {
                        accountLevel = 1;
                    }
                }
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
                isUserRegistered = false;
                balance = 0;
                cardId = null;
                accountLevel = 0;
                amountTransactions = 0;
            }

            return (isUserRegistered, balance, cardId, accountLevel,amountTransactions);
        }
    }
}