using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Seat
    {

        public int SeatId { get; set; }

        public int RowNumber { get; set; }

        public int SeatNumber { get; set; }

        public bool Reserved { get; set; }
    }
}
