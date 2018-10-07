using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Room
    {

        public int RoomId { get; set; }

        public int RoomNumber { get; set; }

        public int NumberOfSeats { get; set; }

        public List<Seat> Seats { get; set; }
    }
}
