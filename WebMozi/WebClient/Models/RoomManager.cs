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

        public RoomManager()
        {
            rooms = new List<DTO.Room>();
        }


        public IEnumerable<DTO.Room> ListRooms()
        {
            return rooms;
        }
        public void CreateRoom(DTO.Room r)
        {
            r.RoomId = roomIDs;
            roomIDs++;
            r.Seats = AddSeats(r.Capacity);
            rooms.Add(r);
        }
        public DTO.Room SelectRoom(int id)
        {
            DTO.Room room = null;
            foreach (DTO.Room r in rooms)
            {
                if (r.RoomId == id)
                {
                    room = r;
                }
            }
            return room;
        }
        public void DeleteRoom(int id)
        {
            foreach (DTO.Room r in rooms.ToList())
            {
                if (r.RoomId == id)
                {
                    rooms.RemoveAt(id);
                }
            }
        }


        public IEnumerable<DTO.Seat> ListSeatsInRoom(int roomID)
        {
            return SelectRoom(roomID).Seats;
        }
        public List<DTO.Seat> AddSeats(int capacity)
        {
            List<DTO.Seat> seats = new List<DTO.Seat>();
            for (int i = 0; i < capacity; i++)
            {
                DTO.Seat seat = new DTO.Seat();
                seat.SeatId = i + 1;
                seat.RowNumber = (i / 10)+1;
                seat.SeatNumber = i + 1;
                seat.IsEnable = true;
                seats.Add(seat);
            }
            return seats;
        }
        public DTO.Seat GetSeat(int seatID,int roomID)
        {
            DTO.Room room = SelectRoom(roomID);
            foreach (DTO.Seat s in room.Seats.ToList()) 
            {
                if (s.SeatId == seatID) {
                    s.IsEnable = false;
                    return s;
                }
            }
            return null;
        }
        
    }
}
