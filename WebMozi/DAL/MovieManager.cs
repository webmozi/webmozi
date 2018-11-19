using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class MovieManager
    {
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


        public static List<Movie> ListMovies()
        {
            using (var context = new CinemaContext())
            {
                return context.Movies
                    .ToList();
            }
        }


    }
}
