using System;

namespace Portal.Business.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
        public int? ProductId { get; set; }
        public string TransactionType { get; set; }
        public string ProviderName { get; set; }
        public string ProviderTransactionId { get; set; }
        public string EmployeeEmail { get; set; }
    }
}