using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MovieEvent
    {
        public Room Room { get; set; }    
        public Movie Movie { get; set; }
        public string Time { get; set; }
        public int ID { get; set; }
      
    }
}
