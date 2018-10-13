using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private List<DTO.Movie> movielist;
        private DAL.CinemaManager cinemamanager;
        private List<DAL.Movie> dallist;

        public ValuesController()
        {
            movielist = new List<DTO.Movie>();
            cinemamanager = new DAL.CinemaManager();
            dallist = cinemamanager.ListMovies();

            foreach(var movie in dallist)
            {
                movielist.Add(new DTO.Movie
                {
                    Director = movie.Director,
                    MovieId = movie.MovieId,
                    Title = movie.Title                 
                });
            }
        }

        [HttpGet]
        public ActionResult<List<DTO.Movie>> Get()
        {                        
            return movielist;
        }

        [HttpGet("{id}")]
        public ActionResult<DTO.Movie> GetById(int id)
        {
            var item = movielist.ElementAt(id);
            if (item == null)
            {
                return NotFound();
            }
            var item2 = new DTO.Movie();
            item2.MovieId = item.MovieId;
            item2.Director = item.Director;
            item2.Title = item.Title;
            return item2;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            movielist.RemoveAt(id);
            dallist.RemoveAt(id);
            cinemamanager.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(DTO.Movie item)
        {
            item.MovieId = movielist.Count();
            movielist.Add(item);
            var dalitem = new DAL.Movie();
            dalitem.MovieId = item.MovieId;
            dalitem.Director = item.Director;
            dalitem.Title = item.Title;
            dallist.Add(dalitem);
            cinemamanager.AddMovie(dalitem);
            return Created("http://localhost:6544/api/values", item);
        }

        [HttpPut]
        public ActionResult<DTO.Movie> Update(DTO.Movie item)
        {
            var movieDTO = movielist.SingleOrDefault(m => m.MovieId == item.MovieId);
            movieDTO.Title = item.Title;
            movieDTO.Director = item.Director;
            movielist.RemoveAll(m => m.MovieId == item.MovieId);
            movielist.Add(movieDTO);

            var movieDal = dallist.SingleOrDefault(m => m.MovieId == item.MovieId);
            dallist.Remove(movieDal);

            var newDalMovie = new DAL.Movie
            {
                Title = item.Title,
                MovieId = item.MovieId,
                Director = item.Director,
            };
            dallist.Add(newDalMovie);          

            cinemamanager.Update(newDalMovie);

            return NoContent();
        }
    }
}
