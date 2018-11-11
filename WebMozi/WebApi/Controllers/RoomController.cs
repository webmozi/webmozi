using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : Controller
    {

        public RoomController()
        {
        }
        [HttpGet]
        public ActionResult<List<DTO.Room>> Get()
        {
            List<DTO.Room> dtorooms = new List<DTO.Room>();
            List<DAL.Room> dalrooms = DAL.Queries.ListRooms();
            foreach (var room in dalrooms)
            {
                DTO.Room dtoroom = new DTO.Room();
                dtoroom.RoomId = room.RoomId;
                dtoroom.Capacity = room.Capacity;
                dtoroom.RoomNumber = room.RoomNumber;
                dtoroom.Seats = new List<DTO.MovieEventSeat>();
                foreach (var dalseat in room.Seats)
                {
                    DTO.MovieEventSeat s = new DTO.MovieEventSeat();
                    s.SeatId = dalseat.SeatId;
                    s.RowNumber = dalseat.RowNumber;
                    s.SeatNumber = dalseat.SeatNumber;
                    dtoroom.Seats.Add(s);
                }
                dtorooms.Add(dtoroom);
            }
            return dtorooms;
        }
        [HttpGet("{id}")]
        public ActionResult<DTO.Room> GetById(int id)
        {
            DTO.Room dtoroom = new DTO.Room();
            dtoroom.Seats = new List<DTO.MovieEventSeat>();
            DAL.Room dalroom= DAL.Queries.GetRoomById(id);
            dtoroom.RoomId = dalroom.RoomId;
            dtoroom.Capacity = dalroom.Capacity;
            dtoroom.RoomNumber = dalroom.RoomNumber;
            dtoroom.Seats = new List<DTO.MovieEventSeat>();
            foreach (var dalseat in dalroom.Seats)
             {
                DTO.MovieEventSeat s = new DTO.MovieEventSeat();
                s.SeatId = dalseat.SeatId;
                s.RowNumber = dalseat.RowNumber;
                s.SeatNumber = dalseat.SeatNumber;
                dtoroom.Seats.Add(s);
             }
            return dtoroom;
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DAL.Administration.DeleteRoom(id);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Create(DTO.Room item)
        {
            var dalitem = new DAL.Room();
            dalitem.RoomId = item.RoomId;
            dalitem.RoomNumber = item.RoomNumber;
            dalitem.Capacity = item.Capacity;
            DAL.Administration.AddRoom(dalitem);
            return Created("http://localhost:6544/api/rooms", item);
        }

    }
}
