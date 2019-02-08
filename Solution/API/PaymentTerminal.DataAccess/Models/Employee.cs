using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentTerminal.DataAccess.Models
{
    public class Employee
    {
        [Key]
        public string Email { get; set; }

        public string CardUid { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}