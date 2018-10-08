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
        private List<DTO.Movie> movielist = new List<DTO.Movie>();

        [HttpGet]
        public ActionResult<List<DTO.Movie>> Get()
        {
            movielist.Add(new DTO.Movie { Title = "Faszom", Director = "Ebbe" , MovieId =0});
            return movielist;
        }
    }
}
