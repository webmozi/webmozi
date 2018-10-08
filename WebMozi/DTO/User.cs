using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
