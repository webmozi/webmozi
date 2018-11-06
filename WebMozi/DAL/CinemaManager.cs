using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class CinemaManager
    {

        public List<Movie> ListMovies()
        {
            using (var context = new CinemaContext())
            {
                return context.Movies
                    .ToList();
            }
        }
        public void AddMovie(Movie movie)
        {
            using (var context = new CinemaContext())
            {
                context.Movies
                    .Add(movie);

                context.SaveChanges();
            }
        }
        public void AddUser(User user)
        {
            using (var context = new CinemaContext())
            {
                context.Users
                    .Add(user);

                context.SaveChanges();
            }
        }
        public void UpdateMovie(DAL.Movie movie)
        {
            using (var context = new CinemaContext())
            {
                var item = context.Movies.Find(movie.MovieId);
                if (item == null)
                {
                    return;
                }
                item.MovieId = movie.MovieId;
                item.Director = movie.Director;
                item.Title = movie.Title;
                context.Movies.Update(item);
                context.SaveChanges();

            }
        }
        public void UpdateUser(DAL.User user)
        {
            using (var context = new CinemaContext())
            {
                var item = context.Users.Find(user.UserId);
                if (item == null)
                {
                    return;
                }
                item.UserId = user.UserId;
                item.Name = user.Name;
                item.TelephoneNumber = user.TelephoneNumber;
                item.Email = user.Email;
                context.Users.Update(item);
                context.SaveChanges();

            }
        }
        public void DeleteMovie(int ig)
        {
            using (var context = new CinemaContext())
            {
                var item = context.Movies.SingleOrDefault(m=>m.MovieId == ig);
                if (item == null)
                {
                    return;
                }
                context.Movies.Remove(item);
                context.SaveChanges();
            }
        }





        public List<Room> ListRooms()
        {
            using (var context = new CinemaContext())
            {
                var AllRooms = context.CinemaRooms.Include(r => r.Seats);
                return AllRooms.ToList();
            }
        }

        public DAL.Room GetRoomById(int id)
        {
            using (var context = new CinemaContext())
            {
                var AllRooms = context.CinemaRooms.Include(r => r.Seats);
                return AllRooms.SingleOrDefault(r => r.RoomId == id);
            }
        }
        public DAL.Movie GetMovieById(int id)
        {
            using (var context = new CinemaContext())
            {
                return context.Movies.SingleOrDefault(m => m.MovieId == id);
            }
        }
        public DAL.User GetUserById(int id)
        {
            using (var context = new CinemaContext())
            {
                return context.Users.SingleOrDefault(u=> u.UserId == id);
            }
        }
        public List<Seat> ListSeatsInMovieEvent(int id)
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
        public void AddRoom(Room room)
        {
            using (var context = new CinemaContext())
            {
                List<DAL.Seat> seats = new List<DAL.Seat>();
                for (int i = 0; i < room.Capacity; i++)
                {
                    DAL.Seat seat = new DAL.Seat();
                    seat.SeatNumber = (i / 10) + 1;
                    seat.RowNumber = i + 1;
                    seats.Add(seat);
                }
                room.Seats = seats;
                context.CinemaRooms
                 .Add(room);

                context.SaveChanges();
            }
        }
        public void DeleteRoom(int id)
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
        public void DeleteUser(int id)
        {
            using (var context = new CinemaContext())
            {
                var item = context.Users.SingleOrDefault(u=> u.UserId == id);
                if (item == null)
                {
                    return;
                }
                context.Users.Remove(item);
                context.SaveChanges();
            }
        }
        public void AddMovieEvent(MovieEvent me)
        {
            using (var context = new CinemaContext())
            {

                context.MovieEvents.Add(me);

                context.SaveChanges();
            }
        }
        public void DeleteMovieEvent(int id)
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


        public List<MovieEvent> ListMovieEvents()
        {
            using( CinemaContext ctx = new CinemaContext())
            {

                return ctx.MovieEvents.ToList();
            }
        }
        public MovieEvent GetMovieEventById(int id) {
            using (CinemaContext ctx = new CinemaContext())
            {
                var AllMoviesEvents = ctx.MovieEvents.Include(me => me.Room)
                                               .Include(me => me.Movie)
                                               .Include(me => me.Room.Seats);
                return AllMoviesEvents.SingleOrDefault(me => me.MovieEventId == id);
            }
         }
        public List<MovieEvent> ListMovieEventsWithRoomAndMovie()
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var AllMoviesEvents = ctx.MovieEvents.Include(me => me.Room)
                                               .Include(me => me.Movie);
                return AllMoviesEvents.ToList();
            }
        }
        public List<Reservation> ListReservations()
        {
            using( CinemaContext ctx = new CinemaContext())
            {
                return ctx.Reservations.ToList();
            }
        }
        public List<User> ListUsers()
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var AllUsers = ctx.Users.Include(u => u.Reservations);
                return AllUsers.ToList();
            }
        }
        public List<User> ListUsersWithoutReservation()
        {
            using (CinemaContext ctx = new CinemaContext())
            {

                return ctx.Users.ToList();
            }
        }
        public List<Seat> ListFreeSeatsForMovieEvent(int movieEventId)
        {
            using( CinemaContext ctx = new CinemaContext() )
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


        public void AddReservation(int movieEventId, int seatId, int userId)
        {
            using( CinemaContext ctx = new CinemaContext() )
            {
                Reservation reservation = new Reservation();
                reservation.MovieEventId = movieEventId;
                reservation.MovieEvent = ctx.MovieEvents.Find(movieEventId);
                reservation.SeatId = seatId;
                reservation.UserId = userId;

                ctx.Reservations.Add(reservation);
                ctx.SaveChanges();
            }
        }


        public List<Reservation> listUserReservations(int userId)
        {
            using( CinemaContext ctx = new CinemaContext() )
            {
                var query = ctx.Reservations
                                .Where(r => r.UserId == userId)
                                .ToList();
                return query;
            }
        }
    }
}
