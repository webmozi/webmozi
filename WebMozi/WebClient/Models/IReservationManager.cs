using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public interface IReservationManager
    {
        IEnumerable<DTO.User> ListUsers();
        IEnumerable<DTO.Reservation> ListReservations();
        DTO.Reservation SelectReservation(int id);
        void AddUser(DTO.User user);
        DTO.User SelectUser(int ID);
        DTO.User EditUser(DTO.User u);
        void DeleteUser(int id);
        int getChosedMovieEventId();
        void SaveMovieEventForReservation(int movieeventid);
        void SaveSeatForReservation(int seatid);
        void MakeReservation();
        void DeleteReservation(int id);
        List<DTO.Reservation> GetReservationsByUser(int userid);
        void LogInUser(DTO.User u);
        void Loggingout();
        int SignedUserId();
    }
}
