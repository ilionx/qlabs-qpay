using System.ComponentModel.DataAnnotations;

namespace Portal.DataAccess.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Productname { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        public Terminal Terminal { get; set; }
    }
}