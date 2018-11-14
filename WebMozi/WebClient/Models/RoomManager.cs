using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class RoomManager
    {
        private List<DTO.Room> rooms;
        private static int roomIDs;
        private static int seatIDs;

        public RoomManager()
        {
            rooms = new List<DTO.Room>();
            roomIDs = 0;
            seatIDs = 0;
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


        public IEnumerable<DTO.MovieEventSeat> ListSeatsInRoom(int roomID)
        {
            return SelectRoom(roomID).Seats;
        }
        public List<DTO.MovieEventSeat> AddSeats(int capacity)
        {
            List<DTO.MovieEventSeat> seats = new List<DTO.MovieEventSeat>();
            for (int i = 0; i < capacity; i++)
            {
                DTO.MovieEventSeat seat = new DTO.MovieEventSeat();
                seat.SeatId = seatIDs;
                seatIDs++;
                seat.SeatNumber = (i / 10) + 1;
                seat.RowNumber = i + 1;
                seats.Add(seat);
            }
            return seats;
        }
        public DTO.MovieEventSeat GetSeat(int seatID, int roomID)
        {
            DTO.Room room = SelectRoom(roomID);
            foreach (DTO.MovieEventSeat s in room.Seats.ToList())
            {
                if (s.SeatId == seatID)
                {
                    return s;
                }
            }
            return null;
        }

    }
}
