using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : Controller
    {
        private List<DTO.Room> dtorooms;
        private DAL.CinemaManager cinemamanager;
        private List<DAL.Room> dalrooms;

        public RoomController()
        {
            dtorooms = new List<DTO.Room>();
            cinemamanager = new DAL.CinemaManager();
            dalrooms = cinemamanager.ListRooms();

            foreach (var movie in dalrooms)
            {
                dtorooms.Add(new DTO.Room
                {
                    Id = movie.RoomId,
                    Capacity = movie.Capacity,
                    RoomNumber = movie.RoomNumber,                    
                });
            }
        }

        [HttpGet]
        public ActionResult<List<DTO.Room>> Get()
        {
            return dtorooms;
        }

        [HttpGet("{id}")]
        public ActionResult<DTO.Room> GetById(int id)
        {
            DTO.Room item = null;
            foreach (DTO.Room m in dtorooms.ToList())
            {
                if (m.Id == id)
                {
                    item = m;
                }
            }
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            for (int i = 0; i < dtorooms.Count; i++)
            {
                if (dtorooms.ElementAt(i).Id == id)
                {
                    // movielist.RemoveAt(i);
                    dalrooms.RemoveAt(i);
                }
            }
            cinemamanager.DeleteRoom(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(DTO.Room item)
        {
            //  item.MovieId = movielist.Count;
            // movielist.Add(item);
            var dalitem = new DAL.Room();
            dalitem.RoomId = item.Id;
            dalitem.RoomNumber = item.RoomNumber;
            dalitem.Capacity = item.Capacity;
            dalrooms.Add(dalitem);
            cinemamanager.AddRoom(dalitem);
            return Created("http://localhost:6544/api/rooms", item);
        }

    }
}
