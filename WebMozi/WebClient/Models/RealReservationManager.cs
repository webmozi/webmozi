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
        }
        private void GetUser()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/user").Result;
            if (result.IsSuccessStatusCode)
            {
                users = result.Content.ReadAsAsync<List<DTO.User>>().Result;
            }
        }
       /* private void GetReservation()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/reservation").Result;
            if (result.IsSuccessStatusCode)
            {
                reservations = result.Content.ReadAsAsync<List<DTO.Reservation>>().Result;
            }
        }*/


        public IEnumerable<DTO.User> ListUsers()
        {
            GetUser();
            return users;
        }
        public void AddUser(DTO.User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.User>("api/user", user).Result;
              
            }
        }
        public DTO.User SelectUser(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.GetAsync("api/user/" + id).Result;
                return response.Content.ReadAsAsync<DTO.User>().Result;

            }
        }
        public void DeleteUser(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.DeleteAsync("api/user/" + (id)).Result;
            }
        }
        public DTO.User EditUser(DTO.User u)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PutAsJsonAsync<DTO.User>("api/user", u).Result;
                if (response.IsSuccessStatusCode)
                {
                    return SelectUser(u.UserId);
                }
                return null;
            }
        }

        public DTO.Reservation SelectReservation(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.GetAsync("api/reservation/" + id).Result;
                return response.Content.ReadAsAsync<DTO.Reservation>().Result;
            }
        }



        public void CreateReservationOnlyWithMovieEvent(DTO.MovieEvent me)
        {
            DTO.Reservation reservation = new DTO.Reservation();
            reservation.MovieEvent = me;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.Reservation>("api/reservation/onlywithmovieevent", reservation).Result;

            }
        }
        public DTO.Reservation AddSeatToReservation(int resID, DTO.MovieEventSeat s)
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
       
        public int getChosedReservationId()
        {
            throw new NotImplementedException();
        }

        public void LogInUser(DTO.User u)
        {
        }

        

        public DTO.User SignedUser()
        {
            return null;
        }

        

        public void Loggingout()
        {
        }

        public IEnumerable<Reservation> ListReservations()
        {
            return null;
        }
    }
}
