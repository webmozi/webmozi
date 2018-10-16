﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MockReservationManager : IReservationManager
    {
        private  List<DTO.User> users;
        private List<DTO.Reservation> reservations;
        private Maker maker;
        private static int reservationIDs = 0;
        private static int userIDs = 0;

        public MockReservationManager() {
            users = new List<DTO.User>();
            reservations = new List<DTO.Reservation>();
            maker = new Maker();
        }
        public int AddReservation(DTO.MovieEvent m,int seatID)
        {
            DTO.Reservation reservation = maker.MakeReservation(m,seatID);
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

        public DTO.User AddUser(DTO.User user)
        {
            user.Id = userIDs;
            userIDs++;
            users.Add(user);
            return user;
        }

        public DTO.User GetUser(int ID)
        {
            DTO.User user = null;
            foreach (DTO.User us in users)
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
