using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public interface IReservationManager
    {
        IEnumerable<Seat> ListSeats();
        IEnumerable<MovieEvent> ListMovies();
        void AddMovie(MovieEvent m);
        void DeleteMovie(int i);

    }
}
