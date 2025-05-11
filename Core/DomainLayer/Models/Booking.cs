using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Booking : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

    }
}
