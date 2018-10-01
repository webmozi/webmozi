using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class Reservation
    {
        public Seat Seat { get; set; }

        public Reservation(Seat seat) {
            Seat = seat;
        }

    }
}
