using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MockReservationManager : IReservationManager
    {
        private  List<User> users = new List<User>();
        private  List<Reservation> reservations = new List<Reservation>();

        private Maker Maker = new Maker();

        public int AddReservation(MovieEvent m) {
            Reservation reservation = Maker.MakeReservation(m);
            reservations.Add(reservation);
            int reservationID = reservations.Count;
            return reservationID;
        }

        public Reservation GetReservation(int resID) {
            return reservations.ElementAt(resID);
        }

        public int AddUser(User user) {
            users.Add(user);
            int userID = users.Count-1;
            return userID;
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
