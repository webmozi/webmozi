using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public interface IReservationManager
    {
        int AddReservation(DTO.MovieEvent m,int seatID);
        DTO.Reservation GetReservation(int resID);
        DTO.User AddUser(DTO.User user);
        DTO.User GetUser(int ID);
        void ReservationToUser(int UserID, int ReservationID);
        }
    }
