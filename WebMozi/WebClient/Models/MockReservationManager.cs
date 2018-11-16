using System;
using System.Collections.Generic;
using System.Linq;
using DTO;

namespace WebClient.Models
{
    public class MockReservationManager : IReservationManager
    {
        private List<User> users;
        private List<DTO.Reservation> reservations;
        private static int reservationIDs;
        private static int userIDs;
        private User signedUser;
        private int chosedreservationid;
        public MockReservationManager()
        {
            userIDs = 0;
            chosedreservationid = -1;
            reservationIDs = 0;
            users = new List<User>();
            reservations = new List<DTO.Reservation>();
            User u = new User() { Name = "Ako", Password = "Ako", TelephoneNumber = "06308888888", Email = "ako@hotmail.com" };
            AddUser(u);
            User u2 = new User() { Name = "Gabo", Password = "Gabo", TelephoneNumber = "06207777777", Email = "gabo@gmail.com" };
            AddUser(u2);
        }

        public IEnumerable<User> ListUsers()
        {
            return users;
        }
        public IEnumerable<DTO.Reservation> ListReservations()
        {
            return reservations;
        }
        public void AddUser(User user)
        {
            user.UserId = userIDs;
            userIDs++;
            users.Add(user);
        }
        public User SelectUser(int ID)
        {
            User user = null;
            foreach (User us in users)
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
            foreach (User u in users.ToList())
            {
                if (u.UserId == id)
                {
                    users.Remove(u);
                }
            }
        }
        public User EditUser(User u)
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
        public void LogInUser(User u)
        {
            User user = null;
            foreach (User us in users)
            {
                if (us.Name == u.Name && us.Password == u.Password)
                {
                    user = us;
                }
            }
            if (user != null)
            {
                signedUser = user;
            }
        }
        public void Loggingout()
        {
            signedUser = null;
            chosedreservationid = -1;
        }

        public User SignedUser()
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
        public DTO.Reservation AddSeatToReservation(int resID, DTO.MovieEventSeat s)
        {
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations.ElementAt(i).ReservationId == resID)
                {
                    reservations.ElementAt(i).Seat = s;
                    return reservations.ElementAt(i);
                }
            }
            return null;
        }
        public DTO.Reservation AddUserToReservation(int resID, User u)
        {
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations.ElementAt(i).ReservationId == resID)
                {
                    reservations.ElementAt(i).User = new DTO.User { Name=u.Name,Email=u.Email,TelephoneNumber=u.TelephoneNumber};
                    return reservations.ElementAt(i);
                }
            }
            return null;
        }
        public DTO.Reservation SelectReservation(int id)
        {
            foreach (DTO.Reservation re in reservations.ToList())
            {
                if (re.ReservationId == id)
                {
                    return re;
                }
            }
            return null;
        }


        public Reservation SelectReservationWithMovieEvent(int id)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetReservations()
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetReservationsByUser(int userid)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }



        public void MakeReservation(int meid, int seatid)
        {
            throw new NotImplementedException();
        }

        int IReservationManager.GetIdByUser(DTO.User u)
        {
            throw new NotImplementedException();
        }

        public void MakeReservation(int meid, int seatid, int userid)
        {
            throw new NotImplementedException();
        }

        public int GetIdByUser(User u)
        {
            throw new NotImplementedException();
        }

        public int GetIdByUser(DTO.User u)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DTO.User> IReservationManager.ListUsers()
        {
            throw new NotImplementedException();
        }

        public void AddUser(DTO.User user)
        {
            throw new NotImplementedException();
        }

        DTO.User IReservationManager.SelectUser(int ID)
        {
            throw new NotImplementedException();
        }

        public DTO.User EditUser(DTO.User u)
        {
            throw new NotImplementedException();
        }

       
    }
}
