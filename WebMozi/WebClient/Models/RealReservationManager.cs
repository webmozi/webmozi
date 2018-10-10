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
        private static int reservationIDs = 0;
        private static int userIDs = 0;

        public RealReservationManager()
        {
            GetUser();
            GetReservation();
            users = new List<DTO.User>();
            reservations = new List<DTO.Reservation>();

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
        public int AddReservation(DTO.MovieEvent m)
        {
            DTO.Reservation reservation = maker.MakeReservation(m);
            reservations.Add(reservation);
            int reservationID = reservations.Count;
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

        public int AddUser(DTO.User user)
        {
            user.Id = userIDs;
            userIDs++;
            users.Add(user);
            return user.Id;
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
