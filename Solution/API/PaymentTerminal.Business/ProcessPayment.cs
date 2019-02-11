using PaymentTerminal.Interfaces;

namespace PaymentTerminal.Business
{
    public class ProcessPayment : IProcessPayment
    {
        private readonly IWriteTransaction _writeTransaction;

        public ProcessPayment(IWriteTransaction writeTransaction)
        {
            _writeTransaction = writeTransaction;
        }

        public bool NewPayment(string cardUid, string transactionType, decimal transactionAmount, decimal newBalance, int productId)
        {
            return _writeTransaction.SaveTransaction(cardUid, transactionType, transactionAmount, newBalance, productId);
        }
    }
}