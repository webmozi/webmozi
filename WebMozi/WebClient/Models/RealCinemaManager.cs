using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class RealCinemaManager :ICinemaManager
    {
        private  List<Movie> movies;
        private  List<Room> rooms;
        private  List<MovieEvent> movieevents;

        public RealCinemaManager() {
            /*movies = new List<Movie>();
            rooms = new List<Room>();
            movieevents = new List<MovieEvent>();
            */
            GetMovies();
            GetRooms();
            GetMovieEvents();
        }
        void GetMovieEvents()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/todo").Result;
            if (result.IsSuccessStatusCode)
            {
                movieevents = result.Content.ReadAsAsync<List<MovieEvent>>().Result;
            }
        }
        void GetMovies() {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/ap/todo").Result;
            if (result.IsSuccessStatusCode)
            {
                movies = result.Content.ReadAsAsync<List<Movie>>().Result;
            }
        }
        void GetRooms()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/todo").Result;
            if (result.IsSuccessStatusCode)
            {
                rooms = result.Content.ReadAsAsync<List<Room>>().Result;
            }
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
        public void AddMovieEvent(MovieEvent me)
        {
            movieevents.Add(me);
        }
        public void AddMovie(Movie m)
        {
            m.MovieId = movies.Count + 1;
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
            rooms.Add(room);
        }
        public void CreateMovieEvent(Movie m, String time,Room r) {
            MovieEvent me = new MovieEvent();
            me.Movie = m;
            me.Time = time;
            me.Room = r;
            movieevents.Add(me);
        }
    }
}
