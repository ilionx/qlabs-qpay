using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.DataAccess.Models
{
    public class NewCards
    {
        [Key]
        [Required]
        [Description("The Uid of the scanned card")]
        public string NewCardUid { get; set; }

        [Required]
        [Description("The date and time the card got scanned")]
        public DateTime ScanTime { get; set; }

        [Required]
        [Description("The location/terminal the card was scanned at")]
        public string ScannedAt { get; set; }
    }
}