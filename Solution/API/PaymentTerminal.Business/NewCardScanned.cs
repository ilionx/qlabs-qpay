using PaymentTerminal.DataAccess;
using PaymentTerminal.Interfaces;

namespace PaymentTerminal.Business
{
    public class NewCardScanned : INewCardScanned
    {
        private readonly IWriteNewCard _writeNewCard;
        private readonly GetProduct _getProduct;

        public NewCardScanned(IWriteNewCard writeNewCard, GetProduct getProduct)
        {
            _writeNewCard = writeNewCard;
            _getProduct = getProduct;
        }

        public void SaveNewCard(string cardUid, string deviceUid)
        {
            string scannedAt = _getProduct.GetProductNameFromTerminal(deviceUid);

            _writeNewCard.SaveNewCard(cardUid, scannedAt);
        }
    }
}