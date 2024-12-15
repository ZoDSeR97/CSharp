using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GolfCourseController : ControllerBase
    {
        private readonly MyContext _context;

        public GolfCourseController(MyContext context)
        {
            _context = context;
        }

        // Get all golf courses
        [HttpGet]
        public ActionResult<IEnumerable<GolfCourse>> GetGolfCourses()
        {
            var golfCourses = _context.GolfCourses.ToList();
            return Ok(golfCourses);
        }

        // Get a specific golf course by ID
        [HttpGet("{id}")]
        public ActionResult<GolfCourse> GetGolfCourse(int id)
        {
            var golfCourse = _context.GolfCourses.Find(id);
            if (golfCourse == null)
            {
                return NotFound();
            }

            return Ok(golfCourse);
        }

        // Create a new golf course
        [HttpPost]
        public ActionResult<GolfCourse> CreateGolfCourse([FromBody] GolfCourse golfCourse)
        {
            _context.GolfCourses.Add(golfCourse);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetGolfCourse), new { id = golfCourse.GolfCourseID }, golfCourse);
        }

        // Update an existing golf course
        [HttpPut("{id}")]
        public ActionResult UpdateGolfCourse(int id, [FromBody] GolfCourse golfCourse)
        {
            if (id != golfCourse.GolfCourseID)
            {
                return BadRequest();
            }

            _context.Entry(golfCourse).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // Delete a golf course
        [HttpDelete("{id}")]
        public ActionResult DeleteGolfCourse(int id)
        {
            var golfCourse = _context.GolfCourses.Find(id);
            if (golfCourse == null)
            {
                return NotFound();
            }

            _context.GolfCourses.Remove(golfCourse);
            _context.SaveChanges();
            return NoContent();
        }
    }
}