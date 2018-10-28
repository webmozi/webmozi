using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class CinemaManager
    {

        public void AddMovie(Movie movie)
        {
            using (var context = new CinemaContext())
            {
                context.Movies
                    .Add(movie);

                context.SaveChanges();
            }
        }

        public void AddRoom(Room movie)
        {
            using (var context = new CinemaContext())
            {
                context.CinemaRooms
                    .Add(movie);

                context.SaveChanges();
            }
        }

        public List<Movie> ListMovies()
        {
            using (var context = new CinemaContext())
            {
                return context.Movies
                    .ToList();
            }
        }

        public List<Room> ListRooms()
        {
            using (var context = new CinemaContext())
            {
                return context.CinemaRooms.ToList();
            }
        }

        public void Update(DAL.Movie movie)
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

        public void UpdateRoom(DAL.Room movie)
        {
            using (var context = new CinemaContext())
            {
                var item = context.CinemaRooms.Find(movie.RoomId);
                if (item == null)
                {
                    return;
                }
                item.RoomId = movie.RoomId;
                item.Capacity = movie.Capacity;
                item.RoomNumber = movie.RoomNumber;
                context.CinemaRooms.Update(item);
                context.SaveChanges();
            }
        }

        public void Delete(int ig)
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
    }
}
