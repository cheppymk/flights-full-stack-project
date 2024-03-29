﻿// <auto-generated />
using System;
using Flights.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Flights.Server.Migrations
{
    [DbContext(typeof(Entities))]
    [Migration("20240117232158_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Flights.Domain.Entities.Flight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RemainingNumberOfSeats")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Flights");

                    b.HasData(
                        new
                        {
                            Id = new Guid("40794675-e773-4601-929a-722108e63b81"),
                            Airline = "American Airlines",
                            Price = "300",
                            RemainingNumberOfSeats = 150
                        },
                        new
                        {
                            Id = new Guid("50794675-e773-4601-929a-722108e63b82"),
                            Airline = "Deutsche BA",
                            Price = "400",
                            RemainingNumberOfSeats = 200
                        });
                });

            modelBuilder.Entity("Flights.Server.Domain.Entities.Passenger", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("Flights.Domain.Entities.Flight", b =>
                {
                    b.OwnsOne("Flights.Server.Domain.Entities.TimePlace", "Arrival", b1 =>
                        {
                            b1.Property<Guid>("FlightId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Place")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("Time")
                                .HasColumnType("datetime2");

                            b1.HasKey("FlightId");

                            b1.ToTable("Flights");

                            b1.WithOwner()
                                .HasForeignKey("FlightId");

                            b1.HasData(
                                new
                                {
                                    FlightId = new Guid("40794675-e773-4601-929a-722108e63b81"),
                                    Place = "London",
                                    Time = new DateTime(2022, 7, 15, 20, 45, 0, 0, DateTimeKind.Unspecified)
                                },
                                new
                                {
                                    FlightId = new Guid("50794675-e773-4601-929a-722108e63b82"),
                                    Place = "Paris",
                                    Time = new DateTime(2022, 7, 16, 22, 30, 0, 0, DateTimeKind.Unspecified)
                                });
                        });

                    b.OwnsOne("Flights.Server.Domain.Entities.TimePlace", "Departure", b1 =>
                        {
                            b1.Property<Guid>("FlightId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Place")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("Time")
                                .HasColumnType("datetime2");

                            b1.HasKey("FlightId");

                            b1.ToTable("Flights");

                            b1.WithOwner()
                                .HasForeignKey("FlightId");

                            b1.HasData(
                                new
                                {
                                    FlightId = new Guid("40794675-e773-4601-929a-722108e63b81"),
                                    Place = "New York",
                                    Time = new DateTime(2022, 7, 15, 10, 30, 0, 0, DateTimeKind.Unspecified)
                                },
                                new
                                {
                                    FlightId = new Guid("50794675-e773-4601-929a-722108e63b82"),
                                    Place = "Berlin",
                                    Time = new DateTime(2022, 7, 16, 12, 0, 0, 0, DateTimeKind.Unspecified)
                                });
                        });

                    b.OwnsMany("Flights.Server.Domain.Entities.Booking", "Bookings", b1 =>
                        {
                            b1.Property<Guid>("FlightId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<byte>("NumberOfSeats")
                                .HasColumnType("tinyint");

                            b1.Property<string>("PassengerEmail")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("FlightId", "Id");

                            b1.ToTable("Booking");

                            b1.WithOwner()
                                .HasForeignKey("FlightId");
                        });

                    b.Navigation("Arrival")
                        .IsRequired();

                    b.Navigation("Bookings");

                    b.Navigation("Departure")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
