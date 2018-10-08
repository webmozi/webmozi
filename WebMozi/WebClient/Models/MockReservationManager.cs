using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MockReservationManager : IReservationManager
    {
        private  List<DTO.User> users;
        private List<DTO.Reservation> reservations;
        private Maker maker;

        public MockReservationManager() {
            users = new List<DTO.User>();
            reservations = new List<DTO.Reservation>();
            maker = new Maker();
        }
        public int AddReservation(DTO.MovieEvent m) {
            DTO.Reservation reservation = maker.MakeReservation(m);
            reservations.Add(reservation);
            int reservationID = reservations.Count;
            return reservationID;
        }

        public DTO.Reservation GetReservation(int resID) {
            return reservations.ElementAt(resID);
        }

        public int AddUser(DTO.User user) {
            users.Add(user);
            int userID = users.Count-1;
            return userID;
        }

        public DTO.User GetUser(int ID)
        {     
            return users.ElementAt(ID);
        }

        public void ReservationToUser(int UserID, int ReservationID) {
            DTO.Reservation reservation = GetReservation(ReservationID);
            users.ElementAt(UserID).Reservations.Add(reservation);
        }

    }
}
