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
        DTO.Movie AddMovie(DTO.Movie m);
        void AddMovieEvent(DTO.MovieEvent me);
        void DeleteMovie(int id);
        void DeleteMovieEvent(int id);
        DTO.Movie SelectMovie(int id);
        DTO.MovieEvent SelectMovieEvent(int id);
        DTO.Movie EditMovie(DTO.Movie m);
        DTO.Room SelectRoom(int id);
        IEnumerable<DTO.Room> ListRooms();
        IEnumerable<DTO.Seat> ListSeatsInRoom(int id);
        void AddRoom(DTO.Room r);
        void DeleteRoom(int id);
    }
}
