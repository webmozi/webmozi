using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Reservation
    {
 
        public int ReservationId { get; set; }

        public MovieEvent MovieEvent { get; set; }

        public List<Seat> ReservedSeats { get; set; }
    }
}
