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
        private static int chosedmovieeventid;
        private static int chosedseatid;
        private static int signuserid;

        public RealReservationManager()
        {
            chosedmovieeventid=-1;
            chosedseatid = -1;
            signuserid = -1;
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








        public IEnumerable<Reservation> ListReservations()
        {
            GetReservation();
            return reservations;
        }
        public void DeleteReservation(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.DeleteAsync("api/reservation/" + id).Result;
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
        public void SaveMovieEventForReservation(int movieeventid)
        {

            chosedmovieeventid = movieeventid;
        }
        public int getChosedMovieEventId() {
            return chosedmovieeventid;
        }
        public void SaveSeatForReservation(int seatid)
        {
            chosedseatid = seatid;
        }
        public void MakeReservation() {
            DTO.Reservation res = new DTO.Reservation();
            res.MovieEvent = new DTO.MovieEvent();
            res.MovieEvent.MovieEventId = chosedmovieeventid;
            res.User = new DTO.User();
            res.User.UserId = signuserid;
            res.Seat = new DTO.MovieEventSeat();
            res.Seat.SeatId = chosedseatid;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.Reservation>("api/reservation", res).Result;
            }
            
        }
        public List<DTO.Reservation> GetReservationsByUser(int id) {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/reservation/resbyuser/" + id).Result;
            return result.Content.ReadAsAsync<List<DTO.Reservation>>().Result;
        }
        




        public void LogInUser(DTO.User u)
        {
            GetUser();
            foreach (var user in users)
            {
                if (user.Name == u.Name)
                {
                    signuserid = user.UserId;
                }
            }

        }
        public int SignedUserId()
        {
            return signuserid;
        }
        public void Loggingout()
        {
            signuserid = -1;
        }

      
    }
}
