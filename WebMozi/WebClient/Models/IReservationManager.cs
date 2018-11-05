using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public interface IReservationManager
    {
        IEnumerable<DTO.User> ListUsers();
        void CreateReservationOnlyWithMovieEvent(DTO.MovieEvent me);
        DTO.Reservation GetReservation(int resID);
        void AddUser(DTO.User user);
        DTO.User SelectUser(int ID);
        DTO.User EditUser(DTO.User u);
        void DeleteUser(int id);
        DTO.Reservation AddSeatToReservation(int resID, DTO.MovieEventSeat s);
        DTO.Reservation AddUserToReservation(int resID, DTO.User u);
        void LogInUser(DTO.User u);
        void Loggingout();
        DTO.User SignedUser();
        int getChosedReservationId();
    }
}
