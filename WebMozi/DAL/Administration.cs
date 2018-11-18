using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Administration
    {


        ////////////////////////////////////////////////////////////////////////////////////////////
        ///                                         ROOMS                                        ///
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static void AddRoom(Room room)
        {
            using (var context = new CinemaContext())
            {
                List<DAL.Seat> seats = new List<DAL.Seat>();
                for (int i = 0; i < room.Capacity; i++)
                {
                    DAL.Seat seat = new DAL.Seat();
                    seat.SeatNumber = i + 1;
                    seat.RowNumber =(i / 6) + 1;
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


        ////////////////////////////////////////////////////////////////////////////////////////////
        ///                                      MOVIES                                         ///
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static void AddMovie(Movie movie)
        {
            using (var context = new CinemaContext())
            {
                context.Movies
                    .Add(movie);

                context.SaveChanges();
            }
        }


        public static void UpdateMovie(Movie movie)
        {
            using (var context = new CinemaContext())
            {
                var item = context.Movies.Find(movie.MovieId);
                if (item == null)
                {
                    return;
                }
                item.Director = movie.Director;
                item.Title = movie.Title;
                item.Length = movie.Length;
                item.Img = movie.Img;
                context.Movies.Update(item);
                context.SaveChanges();

            }
        }



        public static void DeleteMovie(int ig)
        {
            using (var context = new CinemaContext())
            {
                var item = context.Movies.SingleOrDefault(m => m.MovieId == ig);
                if (item == null)
                {
                    return;
                }
                context.Movies.Remove(item);
                context.SaveChanges();
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////
        ///                                 MOVIE EVENTS                                        ///
        ///////////////////////////////////////////////////////////////////////////////////////////


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



        ////////////////////////////////////////////////////////////////////////////////////////////
        ///                                       USERS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static void AddUser(User user)
        {
            using (var context = new CinemaContext())
            {
                context.Users
                    .Add(user);

                context.SaveChanges();
            }
        }



        public static void DeleteUser(int id)
        {
            using (var context = new CinemaContext())
            {
                var item = context.Users.SingleOrDefault(u => u.UserId == id);
                if (item == null)
                {
                    return;
                }
                context.Users.Remove(item);
                context.SaveChanges();
            }
        }

        public static void UpdateUser(User user)
        {
            using (var context = new CinemaContext())
            {
                var item = context.Users.Find(user.UserId);
                if (item == null)
                {
                    return;
                }
                item.Name = user.Name;
                item.TelephoneNumber = user.TelephoneNumber;
                item.Email = user.Email;
                context.Users.Update(item);
                context.SaveChanges();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        ///                                       RESERVATIONS                                  ///
        ///////////////////////////////////////////////////////////////////////////////////////////


        public static void AddReservation(Reservation reservation)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                ctx.Reservations.Add(reservation);
                ctx.SaveChanges();
            }
        }
        public static void DeleteReservation(int id)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var item = ctx.Reservations.SingleOrDefault(r => r.ReservationId == id);
                if (item == null)
                {
                    return;
                }
                ctx.Reservations.Remove(item);
                ctx.SaveChanges();
            }
        }

    }
}
