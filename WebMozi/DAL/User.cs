using System;
using System.Collections.Generic;

namespace DAL
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string TelephoneNumber { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
