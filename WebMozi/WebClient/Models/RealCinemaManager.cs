using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class RealCinemaManager :ICinemaManager
    {
        private  List<MovieEvent> movies;
        private  List<Room> rooms;

        public RealCinemaManager() {
            GetMovies();
            GetRooms();
            //Amíg még nem működik
            movies = new List<MovieEvent>();
            rooms = new List<Room>();
            //
        }
        void GetMovies() {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api").Result;
            if (result.IsSuccessStatusCode)
            {
                movies = result.Content.ReadAsAsync<List<MovieEvent>>().Result;
            }
        }
        void GetRooms()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api").Result;
            if (result.IsSuccessStatusCode)
            {
                rooms = result.Content.ReadAsAsync<List<Room>>().Result;
            }
        }

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
            m.ID = movies.Count + 1;
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
