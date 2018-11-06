using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<DTO.User>> Get()
        {
            List<DTO.User> dtouserlist = new List<DTO.User>();
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            List<DAL.User> dalusers = cinemamanager.ListUsersWithoutReservation();
            foreach (var user in dalusers)
            {
                dtouserlist.Add(new DTO.User
                {
                    Name = user.Name,
                    UserId = user.UserId,
                    TelephoneNumber = user.TelephoneNumber,
                    Email = user.Email
                });
            }
            return dtouserlist;
        }
        [HttpGet("{id}")]
        public ActionResult<DTO.User> GetById(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            DAL.User daluser = cinemamanager.GetUserById(id);
            DTO.User dtouser = new DTO.User();
            dtouser.Name = daluser.Name;
            dtouser.UserId = daluser.UserId;
            dtouser.TelephoneNumber = daluser.TelephoneNumber;
            dtouser.Email = daluser.Email;
            return dtouser;
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            cinemamanager.DeleteUser(id);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Create(DTO.User item)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            var dalitem = new DAL.User();
            dalitem.Name = item.Name;
            dalitem.TelephoneNumber = item.TelephoneNumber;
            dalitem.Email = item.Email;
            cinemamanager.AddUser(dalitem);
            return Created("http://localhost:6544/api/user", item);
        }
        [HttpPut]
        public ActionResult<DTO.User> Update(DTO.User item)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();

            var newDalUser = new DAL.User
            {
                UserId = item.UserId,
                Name = item.Name,
                TelephoneNumber = item.TelephoneNumber,
                Email = item.Email,
            };

            cinemamanager.UpdateUser(newDalUser);

            return NoContent();
        }
    }
}