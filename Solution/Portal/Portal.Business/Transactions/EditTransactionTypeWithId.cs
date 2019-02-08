using Portal.DataAccess;
using System;
using System.Threading.Tasks;

namespace Portal.Business
{
    public class EditTransactionTypeWithId
    {
        private readonly EditTransactionType _editTransactionType;

        public EditTransactionTypeWithId(EditTransactionType editTransactionType)
        {
            _editTransactionType = editTransactionType;
        }

        public async Task EditTransaction(string transactionId, string newType)
        {
            await _editTransactionType.EditTransaction(Int32.Parse(transactionId), newType);
        }
    }
}