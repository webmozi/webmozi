using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using DTO;

namespace WebClient.Models
{
    public class RealReservationManager : IReservationManager
    {
        private List<DTO.User> GetUser()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/user").Result;
            if (result.IsSuccessStatusCode)
            {
                return result.Content.ReadAsAsync<List<DTO.User>>().Result;
            }
            return null;
        }
        private List<DTO.Reservation> GetReservation()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/reservation").Result;
            if (result.IsSuccessStatusCode)
            {
                return result.Content.ReadAsAsync<List<DTO.Reservation>>().Result;
            }
            return null;
        }


        public IEnumerable<DTO.User> ListUsers()
        {
            return GetUser();
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

            return GetReservation();
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



        public void MakeReservation(int chosedmovieeventid, int chosedseatid, int userid)
        {
            DTO.Reservation res = new DTO.Reservation();
            res.MovieEvent = new DTO.MovieEvent();
            res.MovieEvent.MovieEventId = chosedmovieeventid;
            res.User = new DTO.User();
            res.User.UserId = userid;
            res.Seat = new DTO.MovieEventSeat();
            res.Seat.SeatId = chosedseatid;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.Reservation>("api/reservation", res).Result;
            }

        }
        public List<DTO.Reservation> GetReservationsByUser(int id)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/reservation/resbyuser/" + id).Result;
            return result.Content.ReadAsAsync<List<DTO.Reservation>>().Result;
        }





        public int GetIdByUser(DTO.User u)
        {
            foreach (var user in GetUser().ToList())
            {
                if (user.Email == u.Email)
                {
                    return user.UserId;
                }
            }
            return -1;

        }





    }
}
