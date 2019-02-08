namespace PaymentTerminal.Interfaces
{
    public interface IWriteNewCard
    {
        void SaveNewCard(string cardUid, string scannedAt);
    }
}