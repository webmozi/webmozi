﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class MovieEvent
    {
        //public Room Room { get; set; }    //Még osztályt nem tudok kiírni weben
        public string MovieName { get; set; }
        public string Time { get; set; }
        public int ID { get; set; }
        public Seat GetSeat() {
            return null;//Room.GetSeat();
        }
    }
}
