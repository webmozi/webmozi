using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    class Queries
    {

        ////////////////////////////////////////////////////////////////////////////////////////////
        ///                                         LISTING                                      ///
        ///////////////////////////////////////////////////////////////////////////////////////////



        public static List<Room> ListRooms()
        {
            using (var context = new CinemaContext())
            {
                var AllRooms = context.CinemaRooms.Include(r => r.Seats);
                return AllRooms.ToList();
            }
        }


        public static List<Movie> ListMovies()
        {
            using (var context = new CinemaContext())
            {
                return context.Movies
                    .ToList();
            }
        }


        public static List<MovieEvent> ListMovieEvents()
        {
            using (CinemaContext ctx = new CinemaContext())
            {

                return ctx.MovieEvents.ToList();
            }
        }




        public static List<MovieEvent> ListMovieEventsWithRoomAndMovie()
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var AllMoviesEvents = ctx.MovieEvents.Include(me => me.Room)
                                               .Include(me => me.Movie);
                return AllMoviesEvents.ToList();
            }
        }



        public static List<Seat> ListSeatsInMovieEvent(int id)
        {
            using (var context = new CinemaContext())
            {
                var allSeatsForMovieEvent = context.MovieEvents
                     .Where(m => m.MovieEventId == id)
                     .Select(m => m.Room.Seats)
                     .FirstOrDefault();

                return allSeatsForMovieEvent.ToList();
            }
        }




        public static List<Seat> ListFreeSeatsForMovieEvent(int movieEventId)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var allSeatsForMovieEvent = ctx.MovieEvents
                    .Where(m => m.MovieEventId == movieEventId)
                    .Select(m => m.Room.Seats).FirstOrDefault();



                var reservedSeatIdsForMovieEvent = from r in ctx.Reservations
                                                   where r.MovieEventId == movieEventId
                                                   select r.SeatId;

                var reservedSeatsForMovieEvent = from s in ctx.Seats
                                                 where reservedSeatIdsForMovieEvent.Contains(s.SeatId)
                                                 select s;

                return (allSeatsForMovieEvent.Except(reservedSeatsForMovieEvent)).ToList();
            }
        }


        // Az összes User-t visszaadja, nem csak a foglalás nélkülieket
        public static List<User> ListUsers/*WithoutReservation*/()
        {
            using (CinemaContext ctx = new CinemaContext())
            {

                return ctx.Users.ToList();
            }
        }


        public static List<Reservation> ListResevation()
        {
            using (var context = new CinemaContext())
            {
                return context.Reservations.ToList();
            }
        }


        // Szükség van itt mindkettő ListReservation()-re?
        public static List<Reservation> ListReservations()
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                return ctx.Reservations.Include(r => r.MovieEvent).Include(r => r.MovieEvent.Movie).ToList();
            }
        }


       


        // Itt a MovieEvent Room-jának az összes Seat-jére miért van szükség?
        public static List<Reservation> ListUserReservations(int userId)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var q = ctx.Reservations.Include(r => r.MovieEvent).Include(r => r.MovieEvent.Movie).
                  Include(r => r.MovieEvent.Room).Include(r => r.MovieEvent.Room.Seats)
                  .Where(r => r.UserId == userId);
                return q.ToList();
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////
        ///                                    FIND BY ID                                       ///
        ///////////////////////////////////////////////////////////////////////////////////////////



        public static Seat GetSeatById(int id)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                return ctx.Seats.Where(s => s.SeatId == id).SingleOrDefault();
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


        public static DAL.Movie GetMovieById(int id)
        {
            using (var context = new CinemaContext())
            {
                return context.Movies.SingleOrDefault(m => m.MovieId == id);
            }
        }



        public static MovieEvent GetMovieEventById(int id)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var AllMoviesEvents = ctx.MovieEvents.Include(me => me.Room)
                                               .Include(me => me.Movie)
                                               .Include(me => me.Room.Seats);
                return AllMoviesEvents.SingleOrDefault(me => me.MovieEventId == id);
            }
        }


        public static DAL.User GetUserById(int id)
        {
            using (var context = new CinemaContext())
            {
                return context.Users.Where(u => u.UserId == id).SingleOrDefault();
            }
        }




        public static Reservation GetReservationById(int id)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var AllReservation = ctx.Reservations;
                return AllReservation.SingleOrDefault(r => r.ReservationId == id);
            }
        }
        
      
    }
}
