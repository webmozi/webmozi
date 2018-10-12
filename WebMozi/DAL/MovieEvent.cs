using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class MovieEvent
    {

        public int MovieEventId { get; set; }

        public DateTime TimeOfEvent { get; set; }




        public Room Room { get; set; }

        public int RoomId { get; set; }

        public Movie Movie { get; set; }

        public int MovieId { get; set; }
    }
}
