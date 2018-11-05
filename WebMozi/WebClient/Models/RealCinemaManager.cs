using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DTO;

namespace WebClient.Models
{
    public class RealCinemaManager :ICinemaManager
    {
        private  List<DTO.Movie> movies;
        private  List<DTO.Room> rooms;
        private  List<DTO.MovieEvent> movieevents;
        private  List<DTO.MovieEventHeader> movieeventheaders;


        private void GetMovieEvents()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/movieevents").Result;
            if (result.IsSuccessStatusCode)
            {
                movieevents = result.Content.ReadAsAsync<List<DTO.MovieEvent>>().Result;
            }
        }
        private void GetMovieEventHeaders()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/movieeventheader").Result;
            if (result.IsSuccessStatusCode)
            {
                movieeventheaders = result.Content.ReadAsAsync<List<DTO.MovieEventHeader>>().Result;
            }
        }
        private void GetMovies() {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/movie").Result;
            if (result.IsSuccessStatusCode)
            {
                movies = result.Content.ReadAsAsync<List<DTO.Movie>>().Result;
            }
        }

        private void GetRooms()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/rooms").Result;
            if (result.IsSuccessStatusCode)
            {
                rooms = result.Content.ReadAsAsync<List<DTO.Room>>().Result;
            }
        }

        

        public IEnumerable<DTO.Movie> ListMovies()
        {
            GetMovies();
            return movies;
        }
        public DTO.Movie AddMovie(DTO.Movie m)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.Movie>("api/movie", m).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<DTO.Movie>().Result;
                }
                return null;
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
        public void DeleteMovie(int id)
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
                var response = client.DeleteAsync("api/movie/" + (id)).Result;
            }
        }
        public DTO.Movie EditMovie(DTO.Movie m)
        {
            int k = 0;
            for (int i = 0; i < movies.Count; i++)
            {
                if (movies.ElementAt(i).MovieId == m.MovieId)
                {
                    movies.ElementAt(i).Director = m.Director;
                    movies.ElementAt(i).Title = m.Title;
                    k = i;
                }
            }
            var item = movies.ElementAt(k);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PutAsJsonAsync<DTO.Movie>("api/movie", item).Result;
                if (response.IsSuccessStatusCode)
                {
                    return SelectMovie(item.MovieId);
                }
                return null;;
            }
        }

        


        public IEnumerable<DTO.Room> ListRooms()
        {
            GetRooms();
            return rooms;
        }
        public void AddRoom(Room r)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.Room>("api/rooms", r).Result;

            }
        }

        public DTO.Room SelectRoom(int id)
        {
            DTO.Room selectroom = null;
            foreach (DTO.Room r in rooms.ToList())
            {
                if (r.RoomId == id)
                {
                    selectroom = r;
                }
            }
            return selectroom;
        }
        public void DeleteRoom(int id)
        {
            foreach (DTO.Room m in rooms.ToList())
            {
                if (m.RoomId == id)
                {
                    rooms.Remove(m);
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.DeleteAsync("api/rooms/" + (id)).Result;
            }
        }
        public IEnumerable<DTO.MovieEventSeat> ListSeatsInRoom(int id)
        {
            return SelectRoom(id).Seats;
        }





        public IEnumerable<DTO.MovieEventHeader> ListMovieEventsWithoutSeats()
        {
            GetMovieEventHeaders();
            return movieeventheaders;
        }
        public void AddMovieEvent(DTO.MovieEvent me)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PostAsJsonAsync<DTO.MovieEvent>("api/movieevents", me).Result;

            }
        }
        public DTO.MovieEvent SelectMovieEvent(int id)
        {
            DTO.MovieEvent selectmovieevent = null;
            foreach (DTO.MovieEvent me in movieevents)
            {
                if (me.MovieEventId == id)
                {
                    selectmovieevent = me;
                }
            }
            return selectmovieevent;
        }
        public void DeleteMovieEvent(int id)
        {
            foreach (DTO.MovieEvent me in movieevents.ToList())
            {
                if (me.MovieEventId == id)
                {
                    movieevents.Remove(me);
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.DeleteAsync("api/movieevents/" + (id)).Result;
            }
        }

    }
}
