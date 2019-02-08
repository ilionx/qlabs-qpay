namespace Portal.Business.Models
{
    public class TerminalAndProduct
    {
        public string TerminalId { get; set; }
        public string TerminalDescription { get; set; }
        public int? ProductId { get; set; }
        public string Productname { get; set; }
        public string ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}