using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class CinemaManager
    {
        public static int AddMovie(Movie movie)
        {
            using (var context = new CinemaContext())
            {
                context.Movies
                    .Add(movie);

                return context.SaveChanges();
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
