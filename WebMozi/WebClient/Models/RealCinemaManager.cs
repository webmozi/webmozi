using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class RealCinemaManager :ICinemaManager
    {
        private static List<DTO.Movie> movies;
        private static  List<DTO.MovieEvent> movieevents;
        private RoomManager roommanager;
        private static int movieIDs = 0;

        public RealCinemaManager() {
            GetMovies();
            GetMovieEvents();
            //movieevents = new List<DTO.MovieEvent>();
           // movies = new List<DTO.Movie>();
            roommanager = new RoomManager();
        }
        private static void GetMovieEvents()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/values").Result;
            if (result.IsSuccessStatusCode)
            {
                movieevents = result.Content.ReadAsAsync<List<DTO.MovieEvent>>().Result;
            }
        }
        private static void GetMovies() {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/values").Result;
            if (result.IsSuccessStatusCode)
            {
                movies = result.Content.ReadAsAsync<List<DTO.Movie>>().Result;
            }
        }
        
        public IEnumerable<DTO.Movie> ListMovies()
        {
            return movies;
        }

        public IEnumerable<DTO.MovieEvent> ListMovieEvents()
        {
            return movieevents;
        }

        public IEnumerable<DTO.Room> ListRooms()
        {
            return roommanager.ListRooms();
        }
        public void EditMovie(DTO.Movie m)
        {
            for(int i=0;i<movies.Count;i++)
            {
                if (movies.ElementAt(i).MovieId == m.MovieId) {
                    movies.ElementAt(i).Director = m.Director;
                    movies.ElementAt(i).Title = m.Title;
                }
            }
        }
        public void AddMovieEvent(DTO.MovieEvent me)
        {
            movieevents.Add(me);
        }
        public void AddMovie(DTO.Movie m)
        {
            m.MovieId = movieIDs;
            movieIDs++; ;
            movies.Add(m);
        }

        public void DeleteMovie(int id)
        {
            foreach (DTO.Movie m in movies.ToList())
            {
                if (m.MovieId == id)
                {
                    movies.Remove(m);
                }
            }
        }

        public void DeleteMovieEvent(int id)
        {
            foreach (DTO.MovieEvent me in movieevents.ToList())
            {
                if (me.ID == id)
                {
                    movieevents.Remove(me);
                }
            }
        }
        public DTO.Movie SelectMovie(int id)
        {
            DTO.Movie selectmovie = null;
            foreach (DTO.Movie m in movies.ToList())
            {
                if (m.MovieId == id)
                {
                    selectmovie = m;
                }
            }
            return selectmovie;
        }
        public DTO.MovieEvent SelectMovieEvent(int id)
        {
            DTO.MovieEvent selectmovieevent = null;
            foreach (DTO.MovieEvent me in movieevents)
            {
                if (me.ID == id)
                {
                    selectmovieevent = me;
                }
            }
            return selectmovieevent;
        }
        public void CreateRoom(int capacity)
        {
            roommanager.CreateRoom(capacity);
        }
    }
}
