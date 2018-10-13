using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MockCinemaManager :ICinemaManager
    {
        private List<DTO.Movie> movies;
        private List<DTO.MovieEvent> movieevents;
        private RoomManager roommanager; 
        private static int movieIDs=0;

        public MockCinemaManager() {
            movies = new List<DTO.Movie>();
            DTO.Movie m1 = new DTO.Movie() { Director = "Akos", Title = "Franciska" };
            DTO.Movie m2 = new DTO.Movie() { Director = "Francoka", Title = "Akimaki" };
            DTO.Movie m3 = new DTO.Movie() { Director = "Frani", Title = "Akobako" };
            AddMovie(m1);
            AddMovie(m2);
            AddMovie(m3);
            movieevents = new List<DTO.MovieEvent>();
            roommanager = new RoomManager();
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
        public void AddMovieEvent(DTO.MovieEvent me) {
            movieevents.Add(me);
        }
        public DTO.Movie AddMovie(DTO.Movie m)
        {
            m.MovieId = movieIDs;
            movieIDs++; ;
            movies.Add(m);
            return m;
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
            return id;
        }
        public DTO.Movie EditMovie(DTO.Movie m)
        {
            for (int i = 0; i < movies.Count; i++)
            {
                if (movies.ElementAt(i).MovieId == m.MovieId)
                {
                    movies.ElementAt(i).Director = m.Director;
                    movies.ElementAt(i).Title = m.Title;
                }
            }

            return m;
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
                if (m.MovieId == id) {
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
