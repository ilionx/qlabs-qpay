using System.ComponentModel.DataAnnotations;

namespace PaymentTerminal.DataAccess.Models
{
    public class Terminal
    {
        [Key]
        public string TerminalId { get; set; }

        public string TerminalDescription { get; set; }

        public int ProductId { get; set; }
        //public Product Product { get; set; }
    }
}