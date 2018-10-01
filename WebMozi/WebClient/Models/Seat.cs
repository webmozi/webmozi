using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class Seat
    {
        public int ID { get; set; }
        public Seat(int id) {
            ID = id;
        }
    }
}
