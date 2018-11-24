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
            List<DAL.User> dalusers = DAL.UserManager.ListUsers();
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
            DAL.User daluser = DAL.UserManager.GetUserById(id);
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
            DAL.UserManager.DeleteUser(id);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Create(DTO.User item)
        {
            var dalitem = new DAL.User();
            dalitem.Name = item.Name;
            dalitem.TelephoneNumber = item.TelephoneNumber;
            dalitem.Email = item.Email;
            DAL.UserManager.AddUser(dalitem);
            return Created("http://localhost:6544/api/user", item);
        }
        [HttpPut]
        public ActionResult<DTO.User> Update(DTO.User item)
        {
            var newDalUser = new DAL.User
            {
                UserId = item.UserId,
                Name = item.Name,
                TelephoneNumber = item.TelephoneNumber,
                Email = item.Email,
            };

            DAL.UserManager.UpdateUser(newDalUser);
            return NoContent();
        }
    }
}