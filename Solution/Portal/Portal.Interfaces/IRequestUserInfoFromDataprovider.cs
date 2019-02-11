namespace Portal.Interfaces
{
    public interface IRequestUserInfoFromDataprovider
    {
        (bool IsUserRegistered, decimal Balance, string CardId, int AccountLevel,int AmountTransactions) RequestUserData(string loggedInUserEmail);
    }
}