using System.ComponentModel.DataAnnotations;

namespace PaymentTerminal.DataAccess.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Productname { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public Terminal Terminal { get; set; }
    }
}