using FluentAssertions;
using PaymentTerminal.Business;
using Xunit;
using static PaymentTerminal.Business.ValidateScan;

namespace QPayApi.UnitTest
{
    public class PaymentTerminalScans
    {
        private CreateFakeDatabaseContext _context = new CreateFakeDatabaseContext();

        [Fact(DisplayName = "A check to see if with the correct data everything works fine")]
        public void CheckComplete_ShouldReturn_ScanOk()
        {
            ValidateScan validateScan = _context.GiveContext();
            string cardUid = "04346C824D5380";
            string deviceUid = "04346C824D538012";

            var result = validateScan.CheckScan(cardUid, deviceUid);

            result.status.Should().Be((int)StatusCode.ScanOk);
            result.message.Should().Be(StatusCode.ScanOk.ToString());
        }

        [Fact(DisplayName = "A check if the system reacts to a wrong or unknown cardId")]
        public void CheckCardId_ShouldReturn_EmployeeCardNotFound()
        {
            ValidateScan validateScan = _context.GiveContext();
            string cardUid = "00000000000001";
            string deviceUid = "04346C824D538012";

            var result = validateScan.CheckScan(cardUid, deviceUid);

            result.status.Should().Be((int)StatusCode.EmployeeCardNotFound);
            result.message.Should().Be(StatusCode.EmployeeCardNotFound.ToString());
        }

        [Fact(DisplayName = "Check to see if the system reacts like it should when the terminalId is not found or there is no product attached to the terminal")]
        public void CheckTerminalAndProduct_ShouldReturn_TerminalNotFoundOrNoProductAttached()
        {
            ValidateScan validateScan = _context.GiveContext();
            string cardUid = "04346C824D5380";
            string deviceUid = "04346C824D538011";

            var result = validateScan.CheckScan(cardUid, deviceUid);

            result.status.Should().Be((int)StatusCode.TerminalNotFoundOrNoProductAttached);
            result.message.Should().Be(StatusCode.TerminalNotFoundOrNoProductAttached.ToString());
        }

        [Fact(DisplayName = "A check to see if the system blocks not valid input based on length")]
        public void CheckForValidInput_ShouldReturn_InvalidInput()
        {
            ValidateScan validateScan = _context.GiveContext();
            string cardUid = "12345";
            string deviceUid = "04346C824D538012";

            var result = validateScan.CheckScan(cardUid, deviceUid);

            result.status.Should().Be((int)StatusCode.InvalidInput);
            result.message.Should().Be(StatusCode.InvalidInput.ToString());
        }

        [Fact(DisplayName = "A check to see if the user/card actually has enough saldo to fulfill the payment")]
        public void CheckSaldoFromUser_ShouldReturn_NotEnoughSaldo()
        {
            ValidateScan validateScan = _context.GiveContext();
            string cardUid = "99999999999999";
            string deviceUid = "04346C824D538012";

            var result = validateScan.CheckScan(cardUid, deviceUid);

            result.status.Should().Be((int)StatusCode.NotEnoughSaldo);
            result.message.Should().Be(StatusCode.NotEnoughSaldo.ToString());
        }
    }
}