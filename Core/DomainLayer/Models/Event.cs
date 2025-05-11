using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Event : BaseEntity<int>
    { 
        public string Titel { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Category { get; set; } = default!;

        public string Location { get; set; } = default!;

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; } = default!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
