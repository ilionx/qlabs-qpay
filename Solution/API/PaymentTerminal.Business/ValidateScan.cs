using PaymentTerminal.Interfaces;

namespace PaymentTerminal.Business
{
    public class ValidateScan
    {
        private readonly ICheckBalance _checkBalance;
        private readonly ICheckTerminal _checkTerminal;
        private readonly IProcessPayment _processPayment;
        private readonly INewCardScanned _newCardScanned;

        public enum StatusCode
        {
            ScanOk = 1,
            NotEnoughSaldo = 2,
            EmployeeCardNotFound = 3,
            TerminalNotFoundOrNoProductAttached = 4,
            InvalidInput = 5,
            ConnectionError = 6
        }

        public ValidateScan(ICheckBalance checkBalance, ICheckTerminal checkTerminal, IProcessPayment processPayment, INewCardScanned newCardScanned)
        {
            _checkTerminal = checkTerminal;
            _checkBalance = checkBalance;
            _processPayment = processPayment;
            _newCardScanned = newCardScanned;
        }

        public (int status, string message) CheckScan(string cardUid, string deviceUid)
        {
            //creating vars that contain the statusCode and message. The vars will be returned to the paymentTerminal
            var scanOk = ((int)StatusCode.ScanOk, StatusCode.ScanOk.ToString());
            var notEnough = ((int)StatusCode.NotEnoughSaldo, StatusCode.NotEnoughSaldo.ToString());
            var employeeCardNotFound = ((int)StatusCode.EmployeeCardNotFound, StatusCode.EmployeeCardNotFound.ToString());
            var terminalNotFoundOrNoProductAttached = ((int)StatusCode.TerminalNotFoundOrNoProductAttached, StatusCode.TerminalNotFoundOrNoProductAttached.ToString());
            var invalidInput = ((int)StatusCode.InvalidInput, StatusCode.InvalidInput.ToString());
            var connectionError = ((int)StatusCode.ConnectionError, StatusCode.ConnectionError.ToString());

            if (cardUid.Length == 14 && deviceUid.Length == 16)
            {
                //length check passed
                //Now check if the card is known in the database
                var returnCheckBalance = _checkBalance.ValidateBalance(cardUid);

                if (returnCheckBalance.CardFound) //This means the user has a card attached, next check if balance >= product price
                {
                    //Getting the product price
                    var returnCheckTerminal = _checkTerminal.ValidateTerminal(deviceUid);

                    if (returnCheckTerminal.TerminalFound && returnCheckTerminal.ProductPrice > 0) //This means the terminal is found and has a price attached to it
                    {
                        //Check if the Balance is greater then the product price
                        var returnCalculateNewBalanceBalance = _checkBalance.CalculateNewBalance(returnCheckBalance.Balance, returnCheckTerminal.ProductPrice);

                        if (returnCalculateNewBalanceBalance.valid)
                        {
                            //Saving the payment in the database
                            bool paymentSuccess = _processPayment.NewPayment(returnCheckBalance.EmployeeEmail, "Payment", returnCheckTerminal.ProductPrice, returnCalculateNewBalanceBalance.newBalance, returnCheckTerminal.ProductId);
                            if (paymentSuccess)
                            {
                                return scanOk;
                            }
                            return connectionError;
                        }
                        return notEnough;
                    }
                    return terminalNotFoundOrNoProductAttached;
                }
                //save card in DB as NewCard
                _newCardScanned.SaveNewCard(cardUid, deviceUid);
                return employeeCardNotFound;
            }
            return invalidInput;
        }
    }
}