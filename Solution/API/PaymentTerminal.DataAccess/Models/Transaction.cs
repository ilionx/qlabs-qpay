using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentTerminal.DataAccess.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
        public int? ProductId { get; set; }
        public string TransactionType { get; set; }
        public string ProviderName { get; set; }
        public string ProviderTransactionId { get; set; }
        public Employee Employee { get; set; }
    }
}