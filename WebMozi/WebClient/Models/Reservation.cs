using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class Reservation
    {
        public int ID { get; set;  }
        public Seat Seat { get; set; }

        public Reservation(int id, Seat seat) {
            ID = id;
            Seat = seat;
        }

    }
}
