using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class MovieEvent
    {

        public int MovieEventId { get; set; }

        public int TimeOfEvent { get; set; }

        public Room Room { get; set; }

        public Movie Movie { get; set; }
    }
}
