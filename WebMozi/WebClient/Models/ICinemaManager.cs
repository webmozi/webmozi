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
        DTO.Movie AddMovie(DTO.Movie m);
        void AddMovieEvent(DTO.MovieEvent me);
        int DeleteMovie(int i);
        void DeleteMovieEvent(int id);
        DTO.Movie SelectMovie(int id);
        DTO.MovieEvent SelectMovieEvent(int id);
        DTO.Movie EditMovie(DTO.Movie m);
        void CreateRoom(DTO.Room r);
        IEnumerable<DTO.Seat> ListSeatsInRoom(int id);
        DTO.Room SelectRoom(int id);
        void DeleteRoom(int id);


    }
}
