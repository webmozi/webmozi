using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Room
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }


        public List<DTO.Seat> Seats { get; set; } = new List<DTO.Seat>();
        public int Capacity { get; set; }

        // Szerintem ez szükségtelen, de látom, hogy használjátok valahol
        public int FreeSeats { get; set; }
    }
}
