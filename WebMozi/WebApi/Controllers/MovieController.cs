﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public MovieController()
        {
        }
        [HttpGet]
        public ActionResult<List<DTO.Movie>> Get()
        {
            List<DTO.Movie> movielist = new List<DTO.Movie>();
            List<DAL.Movie> dallist = DAL.MovieManager.ListMovies();
            foreach (var movie in dallist)
            {
                movielist.Add(new DTO.Movie
                {
                    Director = movie.Director,
                    MovieId = movie.MovieId,
                    Length = movie.Length,
                    Title = movie.Title,
                    Img = movie.Img
                });
            }
            return movielist;
        }
        [HttpGet("{id}")]
        public ActionResult<DTO.Movie> GetById(int id)
        {
            DAL.Movie dalmovie=DAL.MovieManager.GetMovieById(id);
            DTO.Movie dtomovie = new DTO.Movie();
            dtomovie.Director = dalmovie.Director;
            dtomovie.MovieId = dalmovie.MovieId;
            dtomovie.Length = dalmovie.Length;
            dtomovie.Title = dalmovie.Title;
            dtomovie.Img = dalmovie.Img;
            return dtomovie;
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DAL.MovieManager.DeleteMovie(id);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Create(DTO.Movie item)
        {
            var dalitem = new DAL.Movie();
            dalitem.Director = item.Director;
            dalitem.Title = item.Title;
            dalitem.Length = item.Length;
            dalitem.Img = item.Img;
            DAL.MovieManager.AddMovie(dalitem);
            return Created("http://localhost:6544/api/movie", item);
        }
        [HttpPut]
        public ActionResult<DTO.Movie> Update(DTO.Movie item)
        {
            var newDalMovie = new DAL.Movie
            {
                Title = item.Title,
                MovieId = item.MovieId,
                Director = item.Director,
                Length = item.Length,
                Img = item.Img
            };

            DAL.MovieManager.UpdateMovie(newDalMovie);

            return NoContent();
        }
    }
}
