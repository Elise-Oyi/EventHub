using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [MaxLength(6)]
        public string Eventcode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string EventName { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Fees { get; set; }

        public int SeatFilled { get; set; }

        [MaxLength(200)]
        public string Logo { get; set; } = string.Empty;
    }
}
