using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebClient.Models
{
    public interface ICinemaManager
    {
        IEnumerable<DTO.Movie> ListMovies();
        IEnumerable<DTO.MovieEvent> ListMovieEvents();
        IEnumerable<DTO.Room> ListRooms();
        void AddMovie(DTO.Movie m);
        void AddMovieEvent(DTO.MovieEvent me);
        void DeleteMovie(int i);
        void DeleteMovieEvent(int id);
        DTO.Movie SelectMovie(int id);
        DTO.MovieEvent SelectMovieEvent(int id);
        void EditMovie(DTO.Movie m);
        void CreateRoom(int capacity);
    }
}
