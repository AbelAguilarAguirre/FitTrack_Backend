using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutinesController : ControllerBase
    {
        private readonly FitTrackContext _context;

        public RoutinesController(FitTrackContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllRoutines()
        {
            var routines = _context.Routines.ToList();

            // Convert to DTOs
            var routineDtos = routines.Select(r => new RoutineDto
            {
                Id = r.Id,
                user_id = r.user_id,
                activity_id = r.activity_id,
                value = r.value,
                unit = r.unit,
                repetitions = r.repetitions,
                progress = r.progress,
                Date = r.Date,
                Type = r.Type.ToString() // Convert enum to string
            }).ToList();

            return Ok(routineDtos);
        }
        [HttpGet("{id}")]
        public IActionResult GetRoutineById(int id)
        {
            var routine = _context.Routines.Find(id);
            if (routine == null)
            {
                return NotFound();
            }

            // Convert to DTO
            var routineDto = new RoutineDto
            {
                Id = routine.Id,
                user_id = routine.user_id,
                activity_id = routine.activity_id,
                value = routine.value,
                unit = routine.unit,
                repetitions = routine.repetitions,
                progress = routine.progress,
                Date = routine.Date,
                Type = routine.Type.ToString() // Convert enum to string
            };

            return Ok(routineDto);
        }
        [HttpPost]
        public IActionResult CreateRoutine([FromBody] RoutineDto routineDto)
        {
            // Add debugging to see what data we're receiving
            Console.WriteLine($"Creating routine with data:");
            Console.WriteLine($"  user_id: {routineDto.user_id}");
            Console.WriteLine($"  activity_id: {routineDto.activity_id}");
            Console.WriteLine($"  value: {routineDto.value}");
            Console.WriteLine($"  unit: {routineDto.unit}");
            Console.WriteLine($"  Type: {routineDto.Type}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"  Error: {error.ErrorMessage}");
                }
                return BadRequest(ModelState);
            }

            // Check if activity exists
            var activityExists = _context.Activities.Any(a => a.Id == routineDto.activity_id);
            Console.WriteLine($"Activity {routineDto.activity_id} exists: {activityExists}");

            if (!activityExists)
            {
                Console.WriteLine($"Available activities:");
                var activities = _context.Activities.ToList();
                foreach (var activity in activities)
                {
                    Console.WriteLine($"  ID: {activity.Id}, Name: {activity.name}");
                }
                return BadRequest($"Activity with ID {routineDto.activity_id} does not exist.");
            }

            // Convert DTO to Routine model
            var routine = new Routine
            {
                user_id = routineDto.user_id,
                activity_id = routineDto.activity_id,
                value = routineDto.value,
                unit = routineDto.unit,
                repetitions = routineDto.repetitions,
                progress = routineDto.progress,
                Date = routineDto.Date,
                Type = routineDto.Type.ToLower() == "goal" ? RoutineType.goal : RoutineType.done
            };

            _context.Routines.Add(routine);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetRoutineById), new { id = routine.Id }, routine);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateRoutine(int id, [FromBody] RoutineDto updatedRoutineDto)
        {
            var routine = _context.Routines.Find(id);
            if (routine == null)
            {
                return NotFound();
            }            // Update routine properties from DTO
            routine.user_id = updatedRoutineDto.user_id;
            routine.activity_id = updatedRoutineDto.activity_id;
            routine.value = updatedRoutineDto.value;
            routine.unit = updatedRoutineDto.unit;
            routine.repetitions = updatedRoutineDto.repetitions;
            routine.progress = updatedRoutineDto.progress; // Update progress field
            routine.Date = updatedRoutineDto.Date;
            routine.Type = updatedRoutineDto.Type.ToLower() == "goal" ? RoutineType.goal : RoutineType.done;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoutine(int id)
        {
            var routine = _context.Routines.Find(id);
            if (routine == null)
            {
                return NotFound();
            }

            _context.Routines.Remove(routine);
            _context.SaveChanges();
            return NoContent();
        }
    }
}