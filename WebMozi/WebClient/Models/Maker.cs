using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class Maker 
    {
        public Reservation MakeReservation(MovieEvent m)
        {
            Seat seat = m.Room.GetSeat();
            Reservation reservation = new Reservation(seat);
            return reservation;
        }
    }
}
