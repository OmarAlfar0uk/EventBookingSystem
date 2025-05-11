using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class User : BaseEntity<int>
    {
        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string Role { get; set; } = default!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
