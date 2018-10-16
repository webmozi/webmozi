using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class RealReservationManager :IReservationManager
    {
        private List<DTO.User> users;
        private List<DTO.Reservation> reservations;
        private Maker maker;

        public RealReservationManager()
        {
            GetUser();
            GetReservation();
         
            maker = new Maker();
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
        public int AddReservation(DTO.MovieEvent m,int seatID)
        {
            DTO.Reservation reservation = maker.MakeReservation(m, seatID);
            reservations.Add(reservation);
            int reservationID = reservation.Id;
            return reservationID;
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

        public DTO.User AddUser(DTO.User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.User>("api/values", user).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<DTO.User>().Result;
                }
                return null;
            }
        }

        public DTO.User GetUser(int ID)
        {
            DTO.User user = null;
            foreach (DTO.User us in users.ToList())
            {
                if (user.Id == ID)
                {
                    user = us;
                }
            }
            return user;
        }

        public void ReservationToUser(int UserID, int ReservationID)
        {
            GetUser(UserID).Reservations.Add(GetReservation(ReservationID));
        }
    }
}
