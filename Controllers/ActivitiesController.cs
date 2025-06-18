using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly FitTrackContext _context;

        public ActivitiesController(FitTrackContext context)
        {
            _context = context;
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetActivitiesByUser(int userId)
        {
            var activities = _context.Activities
                .Where(a => a.UserId == userId)
                .ToList();
            return Ok(activities);
        }

        [HttpGet]
        public IActionResult GetAllActivities()
        {
            var activities = _context.Activities.ToList();
            return Ok(activities);
        }
        [HttpGet("{id}")]
        public IActionResult GetActivityById(int id)
        {
            var activity = _context.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        [HttpGet("user/{userId}/{id}")]
        public IActionResult GetActivityByIdForUser(int userId, int id)
        {
            var activity = _context.Activities
                .FirstOrDefault(a => a.Id == id && a.UserId == userId);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }
        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] Activity activity)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"{activity.name} is not valid");
                return BadRequest(ModelState);
            }

            // Validate that the user exists
            var user = await _context.Users.FindAsync(activity.UserId);
            if (user == null)
            {
                return BadRequest("Invalid user ID.");
            }

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] Activity updatedActivity)
        {
            if (id != updatedActivity.Id)
            {
                return BadRequest("Activity ID mismatch.");
            }

            var activity = await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == updatedActivity.UserId);
            if (activity == null)
            {
                return NotFound("Activity not found or you don't have permission to update it.");
            }

            activity.name = updatedActivity.name;
            activity.description = updatedActivity.description;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("user/{userId}/{id}")]
        public async Task<IActionResult> DeleteActivityForUser(int userId, int id)
        {
            var activity = await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (activity == null)
            {
                return NotFound("Activity not found or you don't have permission to delete it.");
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}