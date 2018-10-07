using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MockCinemaManager :ICinemaManager
    {
        private List<Movie> movies;
        private List<MovieEvent> movieevents;
        private  List<Room> rooms;

        public MockCinemaManager() {
            movies = new List<Movie>();
            movieevents = new List<MovieEvent>();
            rooms = new List<Room>();
        }
        public IEnumerable<Movie> ListMovies()
        {
            return movies;
        }

        public IEnumerable<MovieEvent> ListMovieEvents()
        {
            return movieevents;
        }

        public IEnumerable<Room> ListRooms()
        {
            return rooms;
        }
        public void AddMovieEvent(MovieEvent me) {
            movieevents.Add(me);
        }
        public void AddMovie(Movie m)
        {
            m.MovieId = movies.Count+1;
            movies.Add(m);
        }

        public void DeleteMovie(int id)
        {
            movies.RemoveAt(id);
        }
        public void DeleteMovieEvent(int id)
        {
            movieevents.RemoveAt(id);
        }
        public Movie SelectMovie(int id)
        {
            return movies.ElementAt(id);
        }
        public void SelectMovieEvent(int id)
        {
            movieevents.ElementAt(id);
        }
        public void CreateRoom(int capacity)
        {
            Room room = new Room(capacity, rooms.Count);
        }
    }
}
