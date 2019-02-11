namespace PaymentTerminal.Interfaces
{
    public interface ICheckTerminal
    {
        (decimal ProductPrice, bool TerminalFound, int ProductId) ValidateTerminal(string deviceUid);
    }
}