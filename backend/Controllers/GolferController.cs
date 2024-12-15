using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GolferController : ControllerBase
    {
        private readonly MyContext _context;

        public GolferController(MyContext context)
        {
            _context = context;
        }

        // Get all golfers
        [HttpGet]
        public ActionResult<IEnumerable<Golfer>> GetGolfers()
        {
            var golfers = _context.Golfers.ToList();
            return Ok(golfers);
        }

        // Get a specific golfer by ID
        [HttpGet("{id}")]
        public ActionResult<Golfer> GetGolfer(string id)
        {
            var golfer = _context.Golfers.Find(id);
            if (golfer == null)
            {
                return NotFound();
            }

            return Ok(golfer);
        }

        // Create a new golfer
        [HttpPost]
        public ActionResult<Golfer> CreateGolfer([FromBody] Golfer golfer)
        {
            _context.Golfers.Add(golfer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetGolfer), new { id = golfer.GolferId }, golfer);
        }

        // Update an existing golfer
        [HttpPut("{id}")]
        public ActionResult UpdateGolfer(int id, [FromBody] Golfer golfer)
        {
            if (id != golfer.GolferId)
            {
                return BadRequest();
            }

            _context.Entry(golfer).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // Delete a golfer
        [HttpDelete("{id}")]
        public ActionResult DeleteGolfer(string id)
        {
            var golfer = _context.Golfers.Find(id);
            if (golfer == null)
            {
                return NotFound();
            }

            _context.Golfers.Remove(golfer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
