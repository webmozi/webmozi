﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    // Package Manager Console COMMANDS
    //      - new migration: Add-Migration      FirstMigration -Context CinemaContext -Project DAL -StartUpProject WebApi
    //      - remove the last migration:        Remove-Migration -Context CinemaContext -Project DAL -StartUpProject WebApi
    //      - drop database:                    drop-database -Context CinemaContext -Project DAL -StartUpProject WebApi
    //
    //Add-Migration      FirstMigration -Context CinemaContext -Project DAL -StartUpProject WebApi
    //
    //      -create database based on the created migrations: update-database -Context CinemaContext -Project DAL -StartUpProject WebApi
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //////////////////////////// MOVIES //////////////////////////////////

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    MovieId = 1,
                    Title = "Venom",
                    Director = "Ruben Fleischer",
                    Length = 120,
                    Img= "venom.jfif"
                },

                new Movie
                {
                    MovieId = 2,
                    Title = "Jhonny English",
                    Director = "David Kerr",
                    Length = 95,
                    Img = "venom.jfif"

                },

                new Movie
                {
                    MovieId = 3,
                    Title = "Peppermint",
                    Director = "Pierre Morel",
                    Length = 110,
                    Img = "venom.jfif"
                }

            );


            //////////////////////////// ROOMS //////////////////////////////////

            modelBuilder.Entity<Room>().HasData(

                new Room
                {
                    RoomId = 1,
                    Capacity = 2,
                    RoomNumber = 1
                },

                new Room
                {
                    RoomId = 2,
                    Capacity = 3,
                    RoomNumber = 2
                }
             );



            //////////////////////////// SEATS //////////////////////////////////

            modelBuilder.Entity<Seat>().HasData(
                new Seat
                {
                    SeatId = 1,
                    RowNumber = 1,
                    SeatNumber = 1,
                    RoomId = 1

                },

                new Seat
                {
                    SeatId = 2,
                    RowNumber = 1,
                    SeatNumber = 2,
                    RoomId = 1

                },

                new Seat
                {
                    SeatId = 3,
                    RowNumber = 1,
                    SeatNumber = 2,
                    RoomId = 1

                },

                new Seat
                {
                    SeatId = 4,
                    RowNumber = 1,
                    SeatNumber = 1,

                    RoomId = 2

                },

                new Seat
                {
                    SeatId = 5,
                    RowNumber = 1,
                    SeatNumber = 2,
                    RoomId = 2

                }

            );


            //////////////////////////// MOVIE EVENTS //////////////////////////////////

            modelBuilder.Entity<MovieEvent>().HasData(
                new MovieEvent
                {
                    MovieEventId = 1,
                    TimeOfEvent = new DateTime(2018, 10, 20, 12, 00, 00),

                    MovieId = 1,
                    RoomId = 2
                },

                new MovieEvent
                {
                    MovieEventId = 2,
                    TimeOfEvent = new DateTime(2018, 10, 22, 14, 00, 00),

                    MovieId = 2,
                    RoomId = 2
                },

                new MovieEvent
                {
                    MovieEventId = 3,
                    TimeOfEvent = new DateTime(2018, 10, 23, 10, 00, 00),

                    MovieId = 3,
                    RoomId = 1
                }
            );


            //////////////////////////// USERS //////////////////////////////////

            modelBuilder.Entity<User>().HasData(
            //itt hiányozna a password, ha itt hozzuk létre
            );



            //////////////////////////// RESERVATIONS //////////////////////////////////

            modelBuilder.Entity<Reservation>().HasData(
            //User nélkül nincs reservation
            );
        }

    }
}
