using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class Room
    {
        private List<Seat> seats = new List<Seat>();
        private int number = 0;
        private readonly int capacity;
        public Room(int capac,int id) {
            capacity = capac;
            for (int i = 0; i < capac; i++) {
                Seat seat = new Seat(i + 1);
                seats.Add(seat);
            }
        }
        public IEnumerable<Seat> ListSeats()
        {
            return seats;
        }

        public Seat GetSeat() {
            Seat seat = null;
            if (number < capacity)
            {
                seat = seats.ElementAt(number);
                number++;
            }
            else {
                //Nincs több szék error
            }
            return seat;
        }
    }
}
