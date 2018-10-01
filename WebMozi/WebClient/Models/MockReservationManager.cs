using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MockReservationManager : IReservationManager
    {
        private int ReservationIDs = 0;
        private int UserIDs = 0;
        private List<User> users = new List<User>();
        private List<Reservation> reservations = new List<Reservation>();

        public int MakeReservation(MovieEvent m) {
            Seat seat = m.GetSeat();
            Reservation reservation = new Reservation(ReservationIDs++,seat);
            reservations.Add(reservation);
            return ReservationIDs;
        }
        public Reservation GetReservation(int resID) {
            return reservations.ElementAt(resID);
        }
       

        public int AddUser(User user) {
            user.ID = UserIDs++;
            users.Add(user);
            return UserIDs;
        }

        public int GetUserIDInList(int ID)
        {
            int userlistid = 0;
            for (int i = 0; i < users.Count; i++) {
                if (users.ElementAt(i).ID.Equals(ID)) {
                    userlistid = i;
                }
            }
            return userlistid;
        }
        public User GetUserInList(int ID)
        {
            int userlistid = 0;
            for (int i = 0; i < users.Count; i++)
            {
                if (users.ElementAt(i).ID.Equals(ID))
                {
                    userlistid = i;
                }
            }
            return users.ElementAt(userlistid);
        }
        public Reservation GetReservationById(int ID)
        {
            int reservationid = 0;
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations.ElementAt(i).ID.Equals(ID))
                {
                    reservationid = i;
                }
            }
            return reservations.ElementAt(reservationid);
        }

        public void ReservationToUser(int UserID, int ReservationID) {
            int UserIdInList = GetUserIDInList(UserID);
            Reservation reservation = GetReservationById(ReservationID);
            users.ElementAt(UserIdInList).AddReservation(reservation);
        }

    }
}
