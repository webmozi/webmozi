using System.Collections.Generic;

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
        void MakeReservation(int meid,int seatid,int userid);
        void DeleteReservation(int id);
        List<DTO.Reservation> GetReservationsByUser(int userid);
        int GetIdByUser(DTO.User u);

    }
}
