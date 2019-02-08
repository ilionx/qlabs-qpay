using Portal.Business.Models;
using Portal.Interfaces;

namespace Portal.Business
{
    public class RequestUserData
    {
        private readonly IRequestUserInfoFromDataprovider _requestUserInfoFromDataprovider;

        public RequestUserData(IRequestUserInfoFromDataprovider requestUserInfoFromDataprovider)
        {
            _requestUserInfoFromDataprovider = requestUserInfoFromDataprovider;
        }

        public (bool IsUserRegistered, decimal Balance, string CardId, int AccountLevel,string AmountTransactions) RequestUser(string employeeEmail)
        {
            var returnGetUserInfo = _requestUserInfoFromDataprovider.RequestUserData(employeeEmail);

            LoggedInUserData.DoesUserExist = returnGetUserInfo.IsUserRegistered;
            LoggedInUserData.Balance = returnGetUserInfo.Balance;
            LoggedInUserData.CardId = returnGetUserInfo.CardId;
            LoggedInUserData.AccountLevel = returnGetUserInfo.AccountLevel;
            if (returnGetUserInfo.AmountTransactions != 0)
            {
                LoggedInUserData.AmountTransactions = "U heeft nog " + returnGetUserInfo.AmountTransactions + " openstaande betaling(en).";
            }
            else
            {
                LoggedInUserData.AmountTransactions = "U heeft geen betaling meer openstaan.";
            }


            return (LoggedInUserData.DoesUserExist, LoggedInUserData.Balance, LoggedInUserData.CardId, LoggedInUserData.AccountLevel, LoggedInUserData.AmountTransactions);
        }
    }
}