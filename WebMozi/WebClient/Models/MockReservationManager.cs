using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MockReservationManager : IReservationManager
    {
        private static List<User> users = new List<User>();
        private static List<Reservation> reservations = new List<Reservation>();

        public int MakeReservation(MovieEvent m) {
            Seat seat = m.GetSeat();
            Reservation reservation = new Reservation(seat);
            reservations.Add(reservation);
            return reservations.Count;
        }

        public Reservation GetReservation(int resID) {
            return reservations.ElementAt(resID);
        }

        public int AddUser(User user) {
            users.Add(user);
            return users.Count;
        }

        public User GetUser(int ID)
        {     
            return users.ElementAt(ID);
        }

        public void ReservationToUser(int UserID, int ReservationID) {
            Reservation reservation = GetReservation(ReservationID);
            users.ElementAt(UserID).AddReservation(reservation);
        }

    }
}
