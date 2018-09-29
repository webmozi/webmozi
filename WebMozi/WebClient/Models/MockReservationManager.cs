using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MockReservationManager : IReservationManager
    {
        private  static List<Seat> seats = new List<Seat>();
        private  static List<MovieEvent> movies = new List<MovieEvent>();
        public IEnumerable<Seat> ListSeats()
        {
            return seats;
        }
        public IEnumerable<MovieEvent> ListMovies()
        {
            return movies;
        }
        public void AddMovie(MovieEvent m)
        {
            movies.Add(m);
        }

        public void DeleteMovie(int i)
        {
            movies.RemoveAt(i);
        }
    }
}
