﻿using Flights.Server.Data;
using Flights.Server.Domain.Errors;
using Flights.Server.Dtos;
using Flights.Server.ReadModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Flights.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly Entities _entities;

        public BookingController(Entities entities)
        {
           _entities = entities;
        }

        [HttpGet("{email}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<BookingRm>>List(string email)
        {
            var bookings = _entities.Flights.ToArray()
               .SelectMany(f => f.Bookings
                   .Where(b => b.PassengerEmail == email)
                   .Select(b => new BookingRm(
                       f.Id,
                       f.Airline,
                       f.Price.ToString(),
                       new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                       new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                       b.NumberOfSeats,
                       email
                       )));

            return Ok(bookings);

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Cancel(BookDto dto)
        {
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);

            var error = flight?.CancelBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if(error == null)
            {
                _entities.SaveChanges();
                return NoContent();
            }

            if(error is NotFoundError)
            {
                return NotFound();
            }

            throw new Exception($"The error of type: {error.GetType().Name} occurred while canceling the booking made by {dto.PassengerEmail}");
        }
    }
}
