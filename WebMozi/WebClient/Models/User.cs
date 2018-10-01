using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class User
    {
        [Required(ErrorMessage = "Please enter your name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email!")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number!")]
        public string Phone { get; set; }

        public List<Reservation> Reservations { get; set;}

        public void AddReservation(Reservation reservation) {
            Reservations.Add(reservation);
        }
        
    }
}
