using System.ComponentModel.DataAnnotations;

namespace Portal.DataAccess.Models
{
    public class Terminal
    {
        [Key]
        [Required]
        public string TerminalId { get; set; }

        [Required]
        public string TerminalDescription { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}