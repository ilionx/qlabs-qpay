using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.DataAccess.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [Description("The email of the user")]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        [Description("The Uid of the access card")]
        public string CardUid { get; set; }

        [Required]
        [Description("Current balance of the user")]
        public decimal Balance { get; set; }

        [Required]
        [Description("This shows if the account has admin rights")]
        [DefaultValue(false)]
        public bool Admin { get; set; }

        [Required]
        [Description("List of transactions")]
        public List<Transaction> Transactions { get; set; }
    }
}