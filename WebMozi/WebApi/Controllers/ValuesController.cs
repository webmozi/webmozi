using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private List<DTO.Movie> _dtolist = new List<DTO.Movie>();


        [HttpGet]
        public ActionResult<List<DTO.Movie>> Get()
        {
            //_dallist.Add(new DAL.Movie { Title = "Item1", Director = "Hnaphajnalig" });
            _dtolist.Add(new DTO.Movie { Title = "Faszom", Director = "Ebbe" });
            return _dtolist;
        }
    }
}
