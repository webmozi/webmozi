using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DTO;

namespace WebClient.Models
{
    public class RealReservationManager :IReservationManager
    {
        private List<DTO.User> users;
        private List<DTO.Reservation> reservations;

        public RealReservationManager()
        {
            GetUser();
            GetReservation();
        }
        private void GetUser()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/values").Result;
            if (result.IsSuccessStatusCode)
            {
                users = result.Content.ReadAsAsync<List<DTO.User>>().Result;
            }
        }
        private void GetReservation()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/values").Result;
            if (result.IsSuccessStatusCode)
            {
                reservations = result.Content.ReadAsAsync<List<DTO.Reservation>>().Result;
            }
        }
        public IEnumerable<DTO.User> ListUsers()
        {
            return users;
        }
        public int CreateReservationOnlyWithMovieEvent(DTO.MovieEvent me)
        {
            DTO.Reservation reservation = new DTO.Reservation();
           /* reservation.Id = reservationIDs;
            reservationIDs++;*/
            reservation.MovieEvent = me;
            reservations.Add(reservation);
            return reservation.Id;
        }

        public DTO.Reservation GetReservation(int resID)
        {
            DTO.Reservation reservation = null;
            foreach (DTO.Reservation re in reservations.ToList())
            {
                if (re.Id == resID)
                {
                    reservation = re;
                }
            }
            return reservation;
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
        public void AddUser(DTO.User user)
        {
            users.Add(user);
        }

        public DTO.User SelectUser(int ID)
        {
            DTO.User user = null;
            foreach (DTO.User us in users.ToList())
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
        public DTO.Reservation AddSeatToReservation(int resID, DTO.Seat s)
        {
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations.ElementAt(i).Id == resID)
                {
                    reservations.ElementAt(i).Seats.ToList().Add(s);
                    return reservations.ElementAt(i);
                }
            }
            return null;
        }

        public Reservation AddUserToReservation(int resID, User u)
        {
            throw new NotImplementedException();
        }
    }
}
