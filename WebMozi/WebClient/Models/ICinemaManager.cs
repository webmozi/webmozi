using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public interface ICinemaManager
    {
        IEnumerable<Movie> ListMovies();
        IEnumerable<MovieEvent> ListMovieEvents();
        IEnumerable<Room> ListRooms();
        void AddMovie(Movie m);
        void AddMovieEvent(MovieEvent me);
        void DeleteMovie(int i);
        void DeleteMovieEvent(int id);
        Movie SelectMovie(int id);
        void SelectMovieEvent(int id);
        void CreateRoom(int capacity);
    }
}
