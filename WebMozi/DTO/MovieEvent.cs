using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class MovieEvent
    {
        public Room Room { get; set; }
        public Movie Movie { get; set; }
        public DateTime Time { get; set; }
        public int MovieEventId { get; set; }

    }
}
