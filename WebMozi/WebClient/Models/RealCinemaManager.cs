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
            var result = client.GetAsync("http://localhost:6544/api/movieevents/movieeventheader").Result;
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.GetAsync("api/movie/" + id).Result;
                return response.Content.ReadAsAsync<DTO.Movie>().Result;

            }
        }
        public void DeleteMovie(int id)
        {
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.DeleteAsync("api/movie/" + (id)).Result;
            }
        }
        public DTO.Movie EditMovie(DTO.Movie m)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.PutAsJsonAsync<DTO.Movie>("api/movie", m).Result;
                if (response.IsSuccessStatusCode)
                {
                    return SelectMovie(m.MovieId);
                }
                return null;
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.GetAsync("api/rooms/" + (id)).Result;
                return response.Content.ReadAsAsync<DTO.Room>().Result;
                
            }
        }
        public void DeleteRoom(int id)
        {
           
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
        public List<DTO.MovieEventSeat> getEnableSeats(int id, List<DTO.Reservation> res)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:6544/api/movieevents/enableseats/" + id).Result;

            return result.Content.ReadAsAsync<List<DTO.MovieEventSeat>>().Result;
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.GetAsync("api/movieevents/" + (id)).Result;
                return response.Content.ReadAsAsync<DTO.MovieEvent>().Result;
            }
        }

        public void DeleteMovieEvent(int id)
        {
          
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6544/");
                var response = client.DeleteAsync("api/movieevents/" + (id)).Result;
            }
        }
        
        
    }
}
