using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public interface ICinemaManager
    {
        IEnumerable<MovieEvent> ListMovies();
        IEnumerable<Room> ListRooms();
        void AddMovie(MovieEvent m);
        void DeleteMovie(int i);
        MovieEvent SelectMovie(int id);
        void CreateRoom(int capacity);
    }
}
