using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public interface IReservationManager
    {
        int MakeReservation(MovieEvent m);
        int AddUser(User user);
        int GetUserIDInList(int ID);
        User GetUserInList(int ID);
        Reservation GetReservationById(int ID);
        void ReservationToUser(int UserID, int ReservationID);
       

        }
    }
