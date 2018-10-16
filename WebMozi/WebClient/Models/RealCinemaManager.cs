using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class RealCinemaManager :ICinemaManager
    {
        private  List<DTO.Movie> movies;
        private  List<DTO.MovieEvent> movieevents;
        private RoomManager roommanager;

        public RealCinemaManager() {
            GetMovies();
            GetMovieEvents();            
          
            roommanager = new RoomManager();
        }
        private void GetMovieEvents()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/values").Result;
            if (result.IsSuccessStatusCode)
            {
                movieevents = result.Content.ReadAsAsync<List<DTO.MovieEvent>>().Result;
            }
        }
        private void GetMovies() {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/values").Result;
            if (result.IsSuccessStatusCode)
            {
                movies = result.Content.ReadAsAsync<List<DTO.Movie>>().Result;
            }
        }
        
        public IEnumerable<DTO.Movie> ListMovies()
        {
            GetMovies();
            return movies;
        }

        public IEnumerable<DTO.MovieEvent> ListMovieEvents()
        {
            GetMovieEvents();
            return movieevents;
        }

        public IEnumerable<DTO.Room> ListRooms()
        {
            return roommanager.ListRooms();
        }
        public DTO.Movie EditMovie(DTO.Movie m)
        {
            int k = 0;
            for(int i=0;i<movies.Count;i++)
            {
                if (movies.ElementAt(i).MovieId == m.MovieId) {                    
                    movies.ElementAt(i).Director = m.Director;
                    movies.ElementAt(i).Title = m.Title;
                    k = i;
                }
            }

            var item = movies.ElementAt(k);

            using ( var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PutAsJsonAsync<DTO.Movie>("api/values", item).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<DTO.Movie>().Result;
                }
                return null;
            }
        }
        public void AddMovieEvent(DTO.MovieEvent me)
        {
            movieevents.Add(me);
            
        }
        public DTO.Movie AddMovie(DTO.Movie m)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.Movie>("api/values", m).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<DTO.Movie>().Result;
                }
                return null;
            }
        }

        public int DeleteMovie(int id)
        {
            foreach (DTO.Movie m in movies.ToList())
            {
                if (m.MovieId == id)
                {
                    movies.Remove(m);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.DeleteAsync("api/values/" + (id)).Result;
                if (response.IsSuccessStatusCode)
                {
                    return id;
                }
                return -1;
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
        public DTO.Room SelectRoom(int id)
        {
            return roommanager.SelectRoom(id);
        }
        public void CreateRoom(DTO.Room r)
        {
            roommanager.CreateRoom(r);
        }
        public IEnumerable<DTO.Seat> ListSeatsInRoom(int id)
        {
            return roommanager.ListSeatsInRoom(id);
        }
        public void DeleteRoom(int id)
        {
            roommanager.DeleteRoom(id);
        }
    }
}
