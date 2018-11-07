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
        private int chosedreservationid;

        public RealReservationManager()
        {
            chosedreservationid = -1;
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
        private void GetReservation()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/reservation").Result;
            if (result.IsSuccessStatusCode)
            {
                reservations = result.Content.ReadAsAsync<List<DTO.Reservation>>().Result;
            }
        }


        public IEnumerable<DTO.User> ListUsers()
        {
            GetUser();
            return users;
        }
        public int AddUser(DTO.User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.User>("api/user", user).Result;
              
            }
            GetUser();
            foreach (var u in users)
            {
                if (u.Name == user.Name) { return u.UserId; }
            }
            return -1;
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








        public IEnumerable<Reservation> ListReservations()
        {
            GetReservation();
            return reservations;
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
         public DTO.Reservation SelectReservationWithMovieEventAndSeat(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.GetAsync("api/reservation/withmeandseat/" + id).Result;
                return response.Content.ReadAsAsync<DTO.Reservation>().Result;
            }
        }
        public Reservation SelectReservationAllIn(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.GetAsync("api/reservation/allin/" + id).Result;
                return response.Content.ReadAsAsync<DTO.Reservation>().Result;
            }
        }
        public DTO.Reservation SelectReservationWithMovieEvent(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.GetAsync("api/reservation/withmovieevent/" + id).Result;
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
        public DTO.Reservation AddSeatToReservation(int resId, DTO.MovieEventSeat s)
        {
            DTO.Reservation reservation= SelectReservationWithMovieEvent(resId);
            reservation.Seat = s;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.Reservation>("api/reservation/seattoreservation", reservation).Result;
            }
            return reservation;
        }
        public DTO.Reservation AddUserToReservation(int resId, DTO.User u)
        {
            DTO.Reservation reservation = SelectReservationWithMovieEventAndSeat(resId);
            reservation.User = u;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.Reservation>("api/reservation/usertoreservation", reservation).Result;
            }
            return reservation;
        }
       
        public int getChosedReservationId()
        {
            return chosedreservationid;
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

        public void setChosedReservationId(int id)
        {
            chosedreservationid = id;
        }
    }
}
