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
        private static int movieeventIDs = 0;

        public MockCinemaManager() {
            movies = new List<DTO.Movie>();
            roommanager = new RoomManager();
            movieevents = new List<DTO.MovieEvent>();
            DTO.Movie m1 = new DTO.Movie() { Title = "Venom", Director = "Ruben Fleischer" };
            DTO.Movie m2 = new DTO.Movie() { Title = "Az első ember", Director = "Damien Chazelle" };
            DTO.Movie m3 = new DTO.Movie() { Title = "Egy kis szívesség", Director = "Paul Feig" };
            AddMovie(m1);
            AddMovie(m2);
            AddMovie(m3);
            DTO.Room r = new DTO.Room() { RoomNumber = 5, Capacity = 20};
            DTO.Room r2 = new DTO.Room() { RoomNumber = 4, Capacity = 30 };
            roommanager.CreateRoom(r);
            roommanager.CreateRoom(r2);
            DTO.MovieEvent me1 = new DTO.MovieEvent();
            me1.Movie = m1;
            me1.Room = roommanager.SelectRoom(r.Id);
            me1.Time = new DateTime(2018, 10, 16, 14, 00, 00);
            DTO.MovieEvent me2 = new DTO.MovieEvent();
            me2.Movie = m2;
            me2.Room = roommanager.SelectRoom(r.Id);
            me2.Time = new DateTime(2018, 10, 16,18,00,00);
            DTO.MovieEvent me3 = new DTO.MovieEvent();
            me3.Movie = m3;
            me3.Room = roommanager.SelectRoom(r2.Id);
            me3.Time = new DateTime(2018, 10, 17, 18, 00, 00);
            AddMovieEvent(me1);
            AddMovieEvent(me2);
            AddMovieEvent(me3);
        }

        public IEnumerable<DTO.Movie> ListMovies()
        {
            return movies;
        }
        public DTO.Movie AddMovie(DTO.Movie m)
        {
            m.MovieId = movieIDs;
            movieIDs++; ;
            movies.Add(m);
            return m;
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
        }
        public DTO.Movie EditMovie(DTO.Movie m)
        {
            int i = -1;
            for (i = 0; i < movies.Count; i++)
            {
                if (movies.ElementAt(i).MovieId == m.MovieId)
                {
                    movies.ElementAt(i).Director = m.Director;
                    movies.ElementAt(i).Title = m.Title;
                }
            }
            return movies.ElementAt(i);
        }



        public IEnumerable<DTO.Room> ListRooms()
        {
            return roommanager.ListRooms();
        }
        public void CreateRoom(DTO.Room r)
        {
            roommanager.CreateRoom(r);
        }
        public DTO.Room SelectRoom(int id)
        {
            return roommanager.SelectRoom(id);
        }
        public void DeleteRoom(int id)
        {
            roommanager.DeleteRoom(id);
        }



        public IEnumerable<DTO.MovieEvent> ListMovieEvents()
        {
            return movieevents;
        }
        public void AddMovieEvent(DTO.MovieEvent me)
        {
            me.ID = movieeventIDs;
            movieeventIDs++; 
            movieevents.Add(me);
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


        public IEnumerable<DTO.Seat> ListSeatsInRoom(int id)
        {
            return roommanager.ListSeatsInRoom(id);
        }
    }
}
