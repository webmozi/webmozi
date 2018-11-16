using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class EnableAndDisableSeats { 
        public List<DTO.MovieEventSeat> AllSeats { get; set; } = new List<DTO.MovieEventSeat>();

        public List<DTO.MovieEventSeat> EnableSeats { get; set; } = new List<DTO.MovieEventSeat>();

        public bool IsEnable { get; set; } = false;

      
    }
}
