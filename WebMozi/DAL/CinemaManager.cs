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

        public List<Movie> ListMovies()
        {
            using (var context = new CinemaContext())
            {
                return context.Movies
                    .ToList();
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
    }
}
