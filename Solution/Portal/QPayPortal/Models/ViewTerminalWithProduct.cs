namespace QPayPortal.Models
{
    public class ViewTerminalWithProduct
    {
        public string TerminalId { get; set; }
        public int? ProductId { get; set; }
        public string Productname { get; set; }
        public string ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}