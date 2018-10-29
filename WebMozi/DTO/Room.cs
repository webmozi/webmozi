using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Room
    {
        public int RoomId { get; set; }

        public int RoomNumber { get; set; }


        public List<DTO.Seat> Seats { get; set; } = new List<DTO.Seat>();
        public int Capacity { get; set; }
        
    }
}
