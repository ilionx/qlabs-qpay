using Microsoft.Extensions.Logging;
using PaymentTerminal.DataAccess;
using PaymentTerminal.Interfaces;

namespace PaymentTerminal.Business
{
    public class CheckBalance : ICheckBalance
    {
        private readonly GetBalance _getBalance;
        private readonly ILogger _logger;

        public CheckBalance(GetBalance getBalance, ILoggerFactory loggerFactory)
        {
            _getBalance = getBalance;
            _logger = loggerFactory.CreateLogger<CheckBalance>();
        }

        public (decimal Balance, bool CardFound, string EmployeeEmail) ValidateBalance(string cardUid)
        {
            decimal balance;
            bool cardFound;
            string employeeEmail;
            //Get balance from database
            try
            {
                var result = _getBalance.GetBalanceFromCard(cardUid);
                balance = result.Balance;
                cardFound = result.ValidCard;
                employeeEmail = result.EmployeeEmail;
            }
            catch (System.Data.SqlClient.SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
                balance = 0;
                cardFound = false;
                employeeEmail = null;
            }

            return (balance, cardFound, employeeEmail);
        }

        public (decimal newBalance, bool valid) CalculateNewBalance(decimal cardBalance, decimal productPrice)
        {
            decimal newBalance;
            bool valid = false;
            if (cardBalance >= productPrice)
            {
                newBalance = cardBalance - productPrice;
                valid = true;
            }
            else
            {
                newBalance = cardBalance;
            }
            return (newBalance, valid);
        }
    }
}