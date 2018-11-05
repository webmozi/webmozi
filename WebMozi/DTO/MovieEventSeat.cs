using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class MovieEventSeat
    {
        public int SeatId { get; set; }

        public int RowNumber { get; set; }

        public int SeatNumber { get; set; }

        public bool IsEnable { get; set; }
    }
}
