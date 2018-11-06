using System;
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
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            List<DAL.Movie> dallist = cinemamanager.ListMovies();
            foreach (var movie in dallist)
            {
                movielist.Add(new DTO.Movie
                {
                    Director = movie.Director,
                    MovieId = movie.MovieId,
                    Title = movie.Title
                });
            }
            return movielist;
        }
        [HttpGet("{id}")]
        public ActionResult<DTO.Movie> GetById(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            DAL.Movie dalmovie=cinemamanager.GetMovieById(id);
            DTO.Movie dtomovie = new DTO.Movie();
            dtomovie.Director = dalmovie.Director;
            dtomovie.MovieId = dalmovie.MovieId;
            dtomovie.Title = dalmovie.Title;
            return dtomovie;
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            cinemamanager.DeleteMovie(id);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Create(DTO.Movie item)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            var dalitem = new DAL.Movie();
            dalitem.Director = item.Director;
            dalitem.Title = item.Title;
            cinemamanager.AddMovie(dalitem);
            return Created("http://localhost:6544/api/movie", item);
        }
        [HttpPut]
        public ActionResult<DTO.Movie> Update(DTO.Movie item)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();

            var newDalMovie = new DAL.Movie
            {
                Title = item.Title,
                MovieId = item.MovieId,
                Director = item.Director,
            };

            cinemamanager.UpdateMovie(newDalMovie);

            return NoContent();
        }
    }
}
