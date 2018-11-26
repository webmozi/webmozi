using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class RoomManager
    {
        public static void AddRoom(Room room)
        {
            using (var context = new CinemaContext())
            {
                List<DAL.Seat> seats = new List<DAL.Seat>();
                for (int i = 0; i < room.Capacity; i++)
                {
                    DAL.Seat seat = new DAL.Seat();
                    seat.SeatNumber = i %6+1;
                    seat.RowNumber = (i / 6) + 1;
                    seats.Add(seat);
                    context.Seats.Add(seat);
                }
                room.Seats = seats;
                context.CinemaRooms
                 .Add(room);
                context.SaveChanges();
            }
        }



        public static void DeleteRoom(int id)
        {
            using (var context = new CinemaContext())
            {
                var item = context.CinemaRooms.SingleOrDefault(m => m.RoomId == id);
                if (item == null)
                {
                    return;
                }
                context.CinemaRooms.Remove(item);
                context.SaveChanges();
            }
        }


        public static List<Room> ListRooms()
        {
            using (var context = new CinemaContext())
            {
                var AllRooms = context.CinemaRooms.Include(r => r.Seats).OrderBy(r=>r.RoomNumber);
                return AllRooms.ToList();
            }
        }



        public static DAL.Room GetRoomById(int id)
        {
            using (var context = new CinemaContext())
            {
                var AllRooms = context.CinemaRooms.Include(r => r.Seats);
                return AllRooms.SingleOrDefault(r => r.RoomId == id);
            }
        }



        public static Seat GetSeatById(int id)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                return ctx.Seats.Where(s => s.SeatId == id).SingleOrDefault();
            }
        }
    }
}
