using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MockCinemaManager :ICinemaManager
    {

        private int RoomIDs = 0;
        public static List<MovieEvent> Movies = new List<MovieEvent>();
        private readonly List<Room> Rooms = new List<Room>();

        public IEnumerable<MovieEvent> ListMovies()
        {
            return Movies;
        }

        public IEnumerable<Room> ListRooms()
        {
            return Rooms;
        }

        public void AddMovie(MovieEvent m)
        {
            Movies.Add(m);
        }

        public void DeleteMovie(int id)
        {
            Movies.RemoveAt(id);
        }

        public MovieEvent SelectMovie(int id)
        {
            return Movies.ElementAt(id);
        }
        public void CreateRoom(int capacity)
        {
            Room room = new Room(capacity, RoomIDs++);
        }
    }
}
