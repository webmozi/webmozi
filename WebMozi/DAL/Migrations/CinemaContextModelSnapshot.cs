﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(CinemaContext))]
    partial class CinemaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Director");

                    b.Property<string>("Title");

                    b.HasKey("MovieId");

                    b.ToTable("Movies");

                    b.HasData(
                        new { MovieId = 1, Director = "Ruben Fleischer", Title = "Venom" },
                        new { MovieId = 2, Director = "David Kerr", Title = "Jhonny English" },
                        new { MovieId = 3, Director = "Pierre Morel", Title = "Peppermint" }
                    );
                });

            modelBuilder.Entity("DAL.MovieEvent", b =>
                {
                    b.Property<int>("MovieEventId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieId");

                    b.Property<int>("RoomId");

                    b.Property<DateTime>("TimeOfEvent");

                    b.HasKey("MovieEventId");

                    b.HasIndex("MovieId");

                    b.HasIndex("RoomId");

                    b.ToTable("MovieEvents");

                    b.HasData(
                        new { MovieEventId = 1, MovieId = 1, RoomId = 2, TimeOfEvent = new DateTime(2018, 10, 20, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { MovieEventId = 2, MovieId = 2, RoomId = 2, TimeOfEvent = new DateTime(2018, 10, 22, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { MovieEventId = 3, MovieId = 3, RoomId = 1, TimeOfEvent = new DateTime(2018, 10, 23, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                    );
                });

            modelBuilder.Entity("DAL.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieEventId");

                    b.Property<int>("SeatId");

                    b.Property<int>("UserId");

                    b.HasKey("ReservationId");

                    b.HasIndex("MovieEventId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");

                    b.HasData(
                        new { ReservationId = 1, MovieEventId = 1, SeatId = 4, UserId = 2 },
                        new { ReservationId = 2, MovieEventId = 1, SeatId = 5, UserId = 2 },
                        new { ReservationId = 3, MovieEventId = 3, SeatId = 1, UserId = 1 }
                    );
                });

            modelBuilder.Entity("DAL.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity");

                    b.Property<int>("RoomNumber");

                    b.HasKey("RoomId");

                    b.ToTable("CinemaRooms");

                    b.HasData(
                        new { RoomId = 1, Capacity = 2, RoomNumber = 1 },
                        new { RoomId = 2, Capacity = 3, RoomNumber = 2 }
                    );
                });

            modelBuilder.Entity("DAL.Seat", b =>
                {
                    b.Property<int>("SeatId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoomId");

                    b.Property<int>("RowNumber");

                    b.Property<int>("SeatNumber");

                    b.HasKey("SeatId");

                    b.HasIndex("RoomId");

                    b.ToTable("Seats");

                    b.HasData(
                        new { SeatId = 1, RoomId = 1, RowNumber = 1, SeatNumber = 1 },
                        new { SeatId = 2, RoomId = 1, RowNumber = 2, SeatNumber = 1 },
                        new { SeatId = 3, RoomId = 1, RowNumber = 3, SeatNumber = 1 },
                        new { SeatId = 4, RoomId = 2, RowNumber = 1, SeatNumber = 1 },
                        new { SeatId = 5, RoomId = 2, RowNumber = 1, SeatNumber = 2 }
                    );
                });

            modelBuilder.Entity("DAL.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("TelephoneNumber");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new { UserId = 1, Email = "laci@gmail.com", Name = "Laci", TelephoneNumber = "06-70-707-0707" },
                        new { UserId = 2, Email = "peti@icloud.com", Name = "Peti", TelephoneNumber = "06-70- 606-0606" }
                    );
                });

            modelBuilder.Entity("DAL.MovieEvent", b =>
                {
                    b.HasOne("DAL.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Reservation", b =>
                {
                    b.HasOne("DAL.MovieEvent", "MovieEvent")
                        .WithMany()
                        .HasForeignKey("MovieEventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Seat", b =>
                {
                    b.HasOne("DAL.Room")
                        .WithMany("Seats")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
