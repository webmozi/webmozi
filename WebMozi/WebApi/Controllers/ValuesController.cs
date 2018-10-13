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
            dallist = new List<DAL.Movie>();
            dallist = cinemamanager.ListMovies();
            for (int i = 0; i < dallist.Count(); i++)
            {
                var movie = new DTO.Movie();
                movie.MovieId = i;
                movie.Title = dallist.ElementAt<DAL.Movie>(i).Title;
                movie.Director = dallist.ElementAt<DAL.Movie>(i).Director;
                movielist.Add(movie);
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
            DTO.Movie dtomovie = null;
            foreach (var x in movielist)
            {
                if (x.MovieId == item.MovieId)
                {
                    dtomovie = new DTO.Movie();
                    dtomovie.MovieId = item.MovieId;
                    dtomovie.Director = item.Director;
                    dtomovie.Title = item.Title;
                }
            }

            if (dtomovie == null)
            {
                return NotFound();
            }

            var item2 = item;
            movielist.Remove(item);
            movielist.Add(dtomovie);

            var dalmovie = new DAL.Movie();
            dalmovie.MovieId = dtomovie.MovieId;
            dalmovie.Director = dtomovie.Director;
            dalmovie.Title = dtomovie.Title;

            dallist.RemoveAt(item.MovieId-1);
            dallist.Add(dalmovie);

            cinemamanager.Update(dalmovie);

            return NoContent();
        }
    }
}
