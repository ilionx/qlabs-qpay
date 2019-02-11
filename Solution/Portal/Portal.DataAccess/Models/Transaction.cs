using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.DataAccess.Models
{
    public class Transaction
    {
        [Key]
        [Required]
        [Description("Transaction ID")]
        public int TransactionId { get; set; }

        [Required]
        [Description("Date and time of the transaction")]
        public DateTime DateTime { get; set; }

        [Required]
        [Description("Amount of the transaction")]
        public decimal Amount { get; set; }

        [Description("The productId of the bought product")]
        public int? ProductId { get; set; }

        [Required]
        [Description("Payment or Topup?")]
        public string TransactionType { get; set; }

        [Description("Name of the used paymentprovider")]
        public string ProviderName { get; set; }

        [Description("Transaction Id from the Paymentprovider")]
        public string ProviderTransactionId { get; set; }

        public string employeeEmail { get; set; }

        [Required]
        [Description("User that made this transaction")]
        public Employee Employee { get; set; }
    }
}