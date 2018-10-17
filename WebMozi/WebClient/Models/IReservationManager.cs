﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public interface IReservationManager
    {
        IEnumerable<DTO.User> ListUsers();
        int CreateReservationOnlyWithMovieEvent(DTO.MovieEvent me);
        DTO.Reservation GetReservation(int resID);
        void AddUser(DTO.User user);
        DTO.User SelectUser(int ID);
        DTO.User EditUser(DTO.User u);
        void DeleteUser(int id);
        DTO.Reservation AddSeatToReservation(int resID, DTO.Seat s);
        DTO.Reservation AddUserToReservation(int resID, DTO.User u);

        }
}
