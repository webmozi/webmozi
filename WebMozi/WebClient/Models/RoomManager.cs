using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class RoomManager
    {
        private List<DTO.Room> rooms;
        private static int roomIDs = 0;
        public IEnumerable<DTO.Room> ListRooms()
        {
            return rooms;
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
                SelectRoom(roomID).Seats.Add(seat);
            }
        }
        public DTO.Room SelectRoom(int id) {
            DTO.Room room = null;
            foreach (DTO.Room r in rooms)
            {
                if (r.Id == id)
                {
                    room = r; 
                }
            }
            return room;
        }
        public IEnumerable<DTO.Seat> ListSeatsInRoom(int roomID)
        {
            return SelectRoom(roomID).Seats;
        }

        public DTO.Seat GetSeat(int roomID)
        {
            DTO.Seat seat = null;
            DTO.Room room = SelectRoom(roomID);
            if (room.FreeSeats < room.Capacity)
            {
                seat = room.Seats.ElementAt(room.FreeSeats);
                room.FreeSeats++;
            }
            else
            {
                //Nincs több szék error
            }
            return seat;
        }
        public void CreateRoom(int capacity) {
            DTO.Room room = new DTO.Room();
            room.Id = roomIDs;
            roomIDs++;
            room.Capacity = capacity;
            rooms.Add(room);
        }
    }
}
