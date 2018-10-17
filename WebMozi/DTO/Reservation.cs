using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Reservation
    {
        public int Id { get; set; }

        public MovieEvent MovieEvent { get; set; }

        public User User { get; set; }

        public List<DTO.Seat> Seats { get; set; } = new List<DTO.Seat>();
    }
}
