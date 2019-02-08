namespace Portal.Interfaces
{
    public interface ISaveCardToUser
    {
        bool CreateNewUserWithCard(string cardId, string userEmail);
    }
}