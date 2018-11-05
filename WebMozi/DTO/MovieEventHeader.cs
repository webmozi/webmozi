using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class MovieEventHeader
    {

        public RoomHeader Room { get; set; }
        public Movie Movie { get; set; }
        public DateTime Time { get; set; }
        public int MovieEventId { get; set; }
    }
}
