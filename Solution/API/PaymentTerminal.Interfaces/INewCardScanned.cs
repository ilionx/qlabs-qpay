namespace PaymentTerminal.Interfaces
{
    public interface INewCardScanned
    {
        void SaveNewCard(string cardUid, string deviceUid);
    }
}