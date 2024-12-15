using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly MyContext _context;

        public TimeSlotController(MyContext context)
        {
            _context = context;
        }

        // GET: api/TimeSlot/{golfCourseID}
        [HttpGet("{golfCourseID}")]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetTimeSlots(int golfCourseID)
        {
            // Fetch time slots for the specified golf course
            var timeSlots = await _context.TimeSlots
                .Where(t => t.GolfCourseID == golfCourseID && t.StartTime >= DateTime.Now)
                .OrderBy(t => t.StartTime)
                .ToListAsync();

            if (timeSlots == null || !timeSlots.Any())
            {
                return NotFound("No available time slots for the selected golf course.");
            }

            return Ok(timeSlots);
        }
    }
}
