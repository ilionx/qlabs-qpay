namespace PaymentTerminal.Interfaces
{
    public interface IWriteTransaction
    {
        bool SaveTransaction(string employeeEmail, string transactionType, decimal transactionAmount, decimal newBalance, int productId);
    }
}