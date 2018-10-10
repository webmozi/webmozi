using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Room
    {
        public int Id { get; set; }
        public List<DTO.Seat> Seats { get; set; } = new List<DTO.Seat>();
        public int Capacity { get; set; }
        public int FreeSeats { get; set; }
    }
}
