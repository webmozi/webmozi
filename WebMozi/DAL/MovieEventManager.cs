using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class MovieEventManager
    {

        public static void AddMovieEvent(MovieEvent me)
        {
            using (var context = new CinemaContext())
            {

                context.MovieEvents.Add(me);

                context.SaveChanges();
            }
        }



        public static void DeleteMovieEvent(int id)
        {
            using (var context = new CinemaContext())
            {
                var item = context.MovieEvents.SingleOrDefault(me => me.MovieEventId == id);
                if (item == null)
                {
                    return;
                }
                context.MovieEvents.Remove(item);
                context.SaveChanges();
            }
        }


        public static List<MovieEvent> ListMovieEvents()
        {
            using (CinemaContext ctx = new CinemaContext())
            {

                return ctx.MovieEvents.OrderBy(me=>me.Movie.Title).ToList();
            }
        }




        public static List<MovieEvent> ListMovieEventsWithRoomAndMovie()
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var AllMoviesEvents = ctx.MovieEvents.Include(me => me.Room)
                                               .Include(me => me.Movie).OrderBy(me=>me.TimeOfEvent).ThenBy(me => me.Movie.Title);
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
                     .FirstOrDefault().OrderBy(o => o.SeatNumber);

                return allSeatsForMovieEvent.ToList();
            }
        }




        public static List<Seat> ListFreeSeatsForMovieEvent(int movieEventId)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var allSeatsForMovieEvent = ctx.MovieEvents
                    .Where(m => m.MovieEventId == movieEventId)
                    .Select(m => m.Room.Seats).FirstOrDefault().OrderBy(o => o.SeatNumber); ;



                var reservedSeatIdsForMovieEvent = from r in ctx.Reservations
                                                   where r.MovieEventId == movieEventId
                                                   select r.SeatId;

                var reservedSeatsForMovieEvent = from s in ctx.Seats
                                                 where reservedSeatIdsForMovieEvent.Contains(s.SeatId)
                                                 select s;

                return (allSeatsForMovieEvent.Except(reservedSeatsForMovieEvent)).OrderBy(o => o.SeatNumber).ToList();
            }
        }
        public static MovieEvent GetMovieEventById(int movieEventId) {
            using (CinemaContext ctx = new CinemaContext()) {
                return ctx.MovieEvents.Include(me=>me.Movie).Include(me=>me.Room).SingleOrDefault(m => m.MovieEventId == movieEventId);
            }
        }
    }
}
