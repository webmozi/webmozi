using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace WebClient.Models
{
    public class MockReservationManager : IReservationManager
    {
        private  List<DTO.User> users;
        private  List<DTO.Reservation> reservations;
        private  int reservationIDs;
        private  int userIDs;
        private DTO.User signedUser;
        private int chosedreservationid;
        public MockReservationManager() {
            userIDs = 0;
            chosedreservationid = -1;
            reservationIDs = 0;
            users = new List<DTO.User>();
            reservations = new List<DTO.Reservation>();
            DTO.User u = new DTO.User() { Name = "Ako", Password="Ako",TelephoneNumber = "06308888888", Email = "ako@hotmail.com" };
            AddUser(u);
            DTO.User u2 = new DTO.User() { Name = "Gabo", Password = "Gabo", TelephoneNumber = "06207777777", Email = "gabo@gmail.com" };
            AddUser(u2);
        }

        public IEnumerable<DTO.User> ListUsers()
        {
            return users;
        }
        public void AddUser(DTO.User user)
        {
            user.UserId = userIDs;
            userIDs++;
            users.Add(user);
        }
        public DTO.User SelectUser(int ID)
        {
            DTO.User user = null;
            foreach (DTO.User us in users)
            {
                if (us.UserId == ID)
                {
                    user = us;
                }
            }
            return user;
        }
        public void DeleteUser(int id)
        {
            foreach (DTO.User u in users.ToList())
            {
                if (u.UserId == id)
                {
                    users.Remove(u);
                }
            }
        }
        public DTO.User EditUser(DTO.User u)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users.ElementAt(i).UserId == u.UserId)
                {
                    users.ElementAt(i).Name = u.Name;
                    users.ElementAt(i).TelephoneNumber = u.TelephoneNumber;
                    users.ElementAt(i).Email = u.Email;
                }
            }
            return u;
        }
        public void LogInUser(DTO.User u)
        {
            DTO.User user = null;
            foreach (DTO.User us in users)
            {
                if (us.Name==u.Name && us.Password==u.Password)
                {
                    user = us;
                }
            }
            if (user != null) {
                signedUser = user;
            }
        }
        public void Loggingout()
        {
            signedUser = null;
            chosedreservationid = -1;
        }
     
        public DTO.User SignedUser()
        {
            return signedUser;
        }

        public void CreateReservationOnlyWithMovieEvent(DTO.MovieEvent me)
        {
            DTO.Reservation reservation = new DTO.Reservation();            
            reservation.ReservationId = reservationIDs;
            reservationIDs++;
            reservation.MovieEvent = me;
            reservations.Add(reservation);
            chosedreservationid = reservation.ReservationId;
        }
        public DTO.Reservation AddSeatToReservation(int resID, DTO.MovieEventSeat s) {
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations.ElementAt(i).ReservationId== resID)
                {
                    reservations.ElementAt(i).Seats.Add(s);
                    return reservations.ElementAt(i);
                }
            }
            return null;
        }
        public DTO.Reservation AddUserToReservation(int resID, DTO.User u)
        {
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations.ElementAt(i).ReservationId == resID)
                {
                    reservations.ElementAt(i).User=u;
                    return reservations.ElementAt(i);
                }
            }
            return null;
        }
        public DTO.Reservation GetReservation(int resID)
        {
            foreach (DTO.Reservation re in reservations.ToList())
            {
                if (re.ReservationId == resID)
                {
                    return re;
                }
            }
            return null;
        }

        public int getChosedReservationId()
        {
            return chosedreservationid;
        }
        
    }
}
