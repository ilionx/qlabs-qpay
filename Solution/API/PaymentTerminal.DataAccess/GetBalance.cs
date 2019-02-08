using System.Linq;

namespace PaymentTerminal.DataAccess
{
    public class GetBalance
    {
        private readonly PaymentTerminalContext _context;

        public GetBalance(PaymentTerminalContext context)
        {
            _context = context;
        }

        public struct GetBalanceResult
        {
            public bool ValidCard { get; set; }
            public decimal Balance { get; set; }
            public string EmployeeEmail { get; set; }
        }

        public GetBalanceResult GetBalanceFromCard(string cardUid)
        {
            var query = from employee in _context.Employees
                        where employee.CardUid == cardUid
                        select employee;

            if (query.Any())
            {
                return new GetBalanceResult
                {
                    Balance = query.First().Balance,
                    EmployeeEmail = query.First().Email,
                    ValidCard = true
                };
            }
            return new GetBalanceResult();
        }
    }
}