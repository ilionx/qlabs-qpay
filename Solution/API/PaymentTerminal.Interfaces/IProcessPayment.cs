namespace PaymentTerminal.Interfaces
{
    public interface IProcessPayment
    {
        bool NewPayment(string cardUid, string transactionType, decimal transactionAmount, decimal newBalance, int productId);
    }
}