using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Reservation
    {
 
        public int ReservationId { get; set; }
        
        public List<Seat> ReservedSeats { get; set; }




        public MovieEvent MovieEvent { get; set; }

        public int MovieEventId { get; set; }

        public int UserId { get; set; }
    }
}
