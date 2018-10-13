﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Reservation
    {
        public int Id { get; set; }

        public MovieEvent MovieEvent { get; set; }

        public Seat Seat { get; set; }

        public User User { get; set; }
    }
}
