using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public class EventDTo
    {
        public int Id { get; set; }

        public string Titel { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Category { get; set; } = default!;

        public string Location { get; set; } = default!;

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; } = default!;
    }
}
