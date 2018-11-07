using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public MovieEvent MovieEvent { get; set; }

        public User User { get; set; }

        public DTO.MovieEventSeat Seat { get; set; }
    }
}
