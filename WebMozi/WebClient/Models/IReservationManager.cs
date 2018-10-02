using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public interface IReservationManager
    {
        int AddReservation(MovieEvent m);
        Reservation GetReservation(int resID);
        int AddUser(User user);
        User GetUser(int ID);
        void ReservationToUser(int UserID, int ReservationID);
        }
    }
