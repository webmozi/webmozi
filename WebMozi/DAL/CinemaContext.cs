using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class CinemaContext : DbContext
    {

        public CinemaContext() : base() { }

        public DbSet<Room> CinemaRooms { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieEvent> MovieEvents { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CinemaDB;Trusted_Connection=True;");
        }

    }
}
