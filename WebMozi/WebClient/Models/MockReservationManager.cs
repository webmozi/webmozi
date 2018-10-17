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
        private  int reservationIDs = 0;
        private  int userIDs = 0;
        
        public MockReservationManager() {
            users = new List<DTO.User>();
            reservations = new List<DTO.Reservation>();
            DTO.User u = new DTO.User() { Name = "Ako", TelephoneNumber = "06307777777", Email = "dj@h.hu" };
            AddUser(u);
        }
        public IEnumerable<DTO.User> ListUsers()
        {
            return users;
        }
        public int CreateReservationOnlyWithMovieEvent(DTO.MovieEvent me)
        {
            DTO.Reservation reservation = new DTO.Reservation();            
            reservation.Id = reservationIDs;
            reservationIDs++;
            reservation.MovieEvent = me;
            reservations.Add(reservation);
            return reservation.Id;
        }
        public DTO.Reservation AddSeatToReservation(int resID, DTO.Seat s) {
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations.ElementAt(i).Id == resID)
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
                if (reservations.ElementAt(i).Id == resID)
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
                if (re.Id == resID)
                {
                    return re;

                }
            }
            return null;
        }

        public void AddUser(DTO.User user)
        {
            user.Id = userIDs;
            userIDs++;
            users.Add(user);
        }

        public DTO.User SelectUser(int ID)
        {
            DTO.User user = null;
            foreach (DTO.User us in users)
            {
                if (us.Id == ID)
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
                if (u.Id == id)
                {
                    users.Remove(u);
                }
            }
        }
     
        public DTO.User EditUser(DTO.User u)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users.ElementAt(i).Id == u.Id)
                {
                    users.ElementAt(i).Name = u.Name;
                    users.ElementAt(i).TelephoneNumber = u.TelephoneNumber;
                    users.ElementAt(i).Email = u.Email;
                }
            }

            return u;
        }
       
    }
}
