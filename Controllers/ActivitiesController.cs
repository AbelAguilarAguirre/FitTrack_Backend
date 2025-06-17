using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public IActionResult CreateActivity([FromBody] Activity activity)
    {
      if (!ModelState.IsValid)
      {
        Console.WriteLine($"{activity.Name} is not valid");
        return BadRequest(ModelState);
      }

      _context.Activities.Add(activity);
      _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateActivity(int id, [FromBody] Activity updatedActivity)
    {
      if (id != updatedActivity.Id)
      {
        return BadRequest("Activity ID mismatch.");
      }

      var activity = _context.Activities.Find(id);
      if (activity == null)
      {
        return NotFound();
      }

      activity.Name = updatedActivity.Name;

      _context.SaveChanges();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteActivity(int id)
    {
      var activity = _context.Activities.Find(id);
      if (activity == null)
      {
        return NotFound();
      }

      _context.Activities.Remove(activity);
      _context.SaveChanges();
      return NoContent();
    }
  }
}