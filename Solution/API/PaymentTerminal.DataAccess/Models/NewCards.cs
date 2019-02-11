using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentTerminal.DataAccess.Models
{
    public class NewCards
    {
        [Key]
        public string NewCardUid { get; set; }

        public DateTime ScanTime { get; set; }
        public string ScannedAt { get; set; }
    }
}