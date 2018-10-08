﻿using System;
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

        public MockCinemaManager() {
            movies = new List<DTO.Movie>();
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
        public void AddMovie(DTO.Movie m)
        {
            m.MovieId = movies.Count;
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
        public DTO.Movie SelectMovie(int id)
        {
            return movies.ElementAt(id);
        }
        public void SelectMovieEvent(int id)
        {
            movieevents.ElementAt(id);
        }
        public void CreateRoom(int capacity)
        {
            roommanager.CreateRoom(capacity);
        }

     
    }
}
