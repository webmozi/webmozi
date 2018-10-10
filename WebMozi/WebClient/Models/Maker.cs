using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class Maker 
    {
        private RoomManager roommanager;
        public void setRoomManager(RoomManager rm) {
            roommanager = rm;
        }
        public DTO.Reservation MakeReservation(DTO.MovieEvent m)
        {
            DTO.Seat seat = roommanager.GetSeat(m.Room.FreeSeats);
            DTO.Reservation reservation = new DTO.Reservation();
            reservation.Seat = seat;
            return reservation;
        }
    }
}
