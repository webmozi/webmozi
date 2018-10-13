using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
