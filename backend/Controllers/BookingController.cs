using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly BookingSystem _bookingSystem;

        public BookingController(MyContext context, BookingSystem bookingSystem)
        {
            _context = context;
            _bookingSystem = bookingSystem;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");

            // Call the BookingSystem to handle booking creation
            var result = await _bookingSystem.CreateBooking(request.GolferId, request.CourseId, request.TimeSlotId);

            if (!result)
                return BadRequest("Unable to create booking. Possible 5-hour rule violation or invalid input.");

            return Ok("Booking created successfully.");
        }

        [HttpPost("Cancel/{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            var result = await _bookingSystem.CancelBooking(bookingId);

            if (!result)
                return NotFound("Booking not found.");

            return Ok("Booking canceled successfully.");
        }
    }

    public class BookingRequest
    {
        public int GolferId { get; set; }
        public int CourseId { get; set; }
        public int TimeSlotId { get; set; }
    }
}
