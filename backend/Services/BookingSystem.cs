using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace backend.Services
{
    public class BookingSystem
    {
        private readonly MyContext _context;

        public BookingSystem(MyContext context)
        {
            _context = context;
        }

        // Create a new booking while enforcing the 5-hour rule
        public async Task<bool> CreateBooking(int golferId, int courseId, int timeSlotId)
        {
            var golfer = await _context.Golfers.FindAsync(golferId);
            if (golfer == null) return false;

            var timeSlot = await _context.TimeSlots.FindAsync(timeSlotId);
            if (timeSlot == null || timeSlot.RemainingCapacity <= 0) return false;

            // Validate the 5-hour rule
            if (!ValidateFiveHourRule(golfer, timeSlot.StartTime))
            {
                return false; // Rule violation, booking denied
            }

            // Create and save booking
            var booking = new Booking
            {
                GolferId = golferId,
                GolfCourseId = courseId,
                TimeSlotId = timeSlotId,
                BookingDateTime = DateTime.UtcNow
            };

            timeSlot.RemainingCapacity--;
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return true;
        }

        // Cancel a booking
        public async Task<bool> CancelBooking(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            var timeSlot = await _context.TimeSlots.FindAsync(booking.TimeSlotId);
            if (timeSlot != null)
            {
                timeSlot.RemainingCapacity++;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if a golfer can make a booking at the proposed time
        public bool ValidateFiveHourRule(Golfer golfer, DateTime proposedBookingTime)
        {
            var activeBookings = _context.Bookings
                .Where(b => b.GolferId == golfer.GolferId)
                .Include(b => b.TimeSlot)
                .ToList();

            foreach (var booking in activeBookings)
            {
                // Check if the proposed time conflicts within the 5-hour rule
                var existingStartTime = booking.TimeSlot.StartTime;
                var existingEndTime = booking.TimeSlot.EndTime;

                if ((proposedBookingTime - existingEndTime).TotalHours < 5 &&
                    (existingStartTime - proposedBookingTime).TotalHours < 5)
                {
                    return false; // Conflicts with an existing booking
                }
            }

            return true; // No conflict
        }
    }
}
