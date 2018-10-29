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
        


        public void AddUser(DTO.User user)
        {
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



        public int CreateReservationOnlyWithMovieEvent(DTO.MovieEvent me)
        {
            DTO.Reservation reservation = new DTO.Reservation();
            reservation.MovieEvent = me;
            reservations.Add(reservation);
            return reservation.ReservationId;
        }
        public DTO.Reservation AddSeatToReservation(int resID, DTO.Seat s)
        {
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations.ElementAt(i).ReservationId == resID)
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
                    reservations.ElementAt(i).User = u;
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
    }
}
