using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MockCinemaManager :ICinemaManager
    {
        
        private static List<MovieEvent> movies = new List<MovieEvent>();
        private static List<Room> rooms = new List<Room>();

        public IEnumerable<MovieEvent> ListMovies()
        {
            return movies;
        }

        public IEnumerable<Room> ListRooms()
        {
            return rooms;
        }

        public void AddMovie(MovieEvent m)
        {
            m.ID = movies.Count+1;
            movies.Add(m);
        }

        public void DeleteMovie(int id)
        {
            movies.RemoveAt(id);
        }

        public MovieEvent SelectMovie(int id)
        {
            return movies.ElementAt(id);
        }

        public void CreateRoom(int capacity)
        {
            Room room = new Room(capacity, rooms.Count);
        }
    }
}
