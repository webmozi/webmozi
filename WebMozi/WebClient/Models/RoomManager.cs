using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class RoomManager
    {
        private List<DTO.Room> rooms;

        public IEnumerable<DTO.Room> ListRooms()
        {
            return rooms.ToArray();
        }

        public RoomManager() {
            rooms = new List<DTO.Room>();
        }
        public void AddSeats(int roomID, int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                DTO.Seat seat = new DTO.Seat();
                seat.ID = i + 1;
                rooms.ElementAt(roomID).Seats.Add(seat);
            }
        }
       
        public IEnumerable<DTO.Seat> ListSeatsInRoom(int roomID)
        {
            return rooms.ElementAt(roomID).Seats;
        }

        public DTO.Seat GetSeat(int roomID)
        {
            DTO.Seat seat = null;
            if (rooms.ElementAt(roomID).Number < rooms.ElementAt(roomID).Capacity)
            {
                seat = rooms.ElementAt(roomID).Seats.ElementAt(rooms.ElementAt(roomID).Number);
                rooms.ElementAt(roomID).Number++;
            }
            else
            {
                //Nincs több szék error
            }
            return seat;
        }
        public void CreateRoom(int capacity) {
            DTO.Room room = new DTO.Room();
            room.Capacity = capacity;
            rooms.Add(room);
        }
    }
}
