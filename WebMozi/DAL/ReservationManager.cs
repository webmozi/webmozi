using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    class ReservationManager
    {

        public static void AddReservation(Reservation reservation)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                ctx.Reservations.Add(reservation);
                ctx.SaveChanges();
            }
        }
        public static void DeleteReservation(int id)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var item = ctx.Reservations.SingleOrDefault(r => r.ReservationId == id);
                if (item == null)
                {
                    return;
                }
                ctx.Reservations.Remove(item);
                ctx.SaveChanges();
            }
        }


        public static List<Reservation> ListReservations()
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                return ctx.Reservations.Include(r => r.MovieEvent).Include(r => r.MovieEvent.Movie).ToList();
            }
        }


        public static Reservation GetReservationById(int id)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var AllReservation = ctx.Reservations;
                return AllReservation.SingleOrDefault(r => r.ReservationId == id);
            }
        }
    }
}
