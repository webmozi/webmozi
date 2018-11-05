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
        private List<DAL.Seat> dalseats;

        public RoomController()
        {
            dtorooms = new List<DTO.Room>();
            cinemamanager = new DAL.CinemaManager();
            dalrooms = cinemamanager.ListRooms();
            dalseats = cinemamanager.ListSeats();

            foreach (var room in dalrooms)
            {
                DTO.Room r = new DTO.Room();
                r.RoomId = room.RoomId;
                r.Capacity = room.Capacity;
                r.RoomNumber = room.RoomNumber;
                r.Seats = new List<DTO.MovieEventSeat>();

                for (int i = 0; i < dalseats.Count; i++) {
                    if (dalseats.ElementAt(i).RoomId == r.RoomId)
                    {
                        DTO.MovieEventSeat s = new DTO.MovieEventSeat();
                        s.SeatId = dalseats.ElementAt(i).SeatId;
                        s.RowNumber = dalseats.ElementAt(i).RowNumber;
                        s.SeatNumber = dalseats.ElementAt(i).SeatNumber;
                        r.Seats.Add(s);
                    }
                }
                dtorooms.Add(r);
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
                if (m.RoomId == id)
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
            dtorooms = new List<DTO.Room>();
            cinemamanager = new DAL.CinemaManager();
            dalrooms = cinemamanager.ListRooms();

            for (int i = 0; i < dtorooms.Count; i++)
            {
                if (dtorooms.ElementAt(i).RoomId == id)
                {
                    dalrooms.RemoveAt(i);
                }
            }
            cinemamanager.DeleteRoom(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(DTO.Room item)
        {
            var dalitem = new DAL.Room();
            dalitem.RoomId = item.RoomId;
            dalitem.RoomNumber = item.RoomNumber;
            dalitem.Capacity = item.Capacity;
            dalrooms.Add(dalitem);
            cinemamanager.AddRoom(dalitem);
            return Created("http://localhost:6544/api/rooms", item);
        }

    }
}
