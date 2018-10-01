using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int ID { get; set; }
        public List<Reservation> Reservations { get; set; }

    }
}
