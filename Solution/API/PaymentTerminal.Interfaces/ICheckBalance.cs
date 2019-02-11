namespace PaymentTerminal.Interfaces
{
    public interface ICheckBalance
    {
        (decimal Balance, bool CardFound, string EmployeeEmail) ValidateBalance(string cardUid);

        (decimal newBalance, bool valid) CalculateNewBalance(decimal cardBalance, decimal productPrice);
    }
}