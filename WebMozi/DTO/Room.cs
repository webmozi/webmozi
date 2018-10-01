using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    class Room
    {
        private readonly List<Seat> seats = new List<Seat>();      
        public int ID { get; set; }
    }
}
