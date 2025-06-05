using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Routine
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserId { get; set; }

        [Required]
        public string ActivityId { get; set; } // Renamed to follow PascalCase

        [Required]
        [MaxLength(50)]
        public string value { get; set; }

        [Required]
        [MaxLength(20)]
        public string unit { get; set; }

        public int repetitions { get; set; }

        public DateTime? Date { get; set; } // Changed to DateTime? for better type safety

        [Required]
        public RoutineType Type { get; set; } // Enum for type safety

        public Routine(string id, string userId, string activityId, string value, string unit, int repetitions = 0, DateTime? date = null, RoutineType type = RoutineType.Done)
        {
            this.Id = id;
            this.UserId = userId;
            this.ActivityId = activityId;
            this.value = value;
            this.unit = unit;
            this.repetitions = repetitions;
            this.Date = date;
            this.Type = type;
        }
    }

    public enum RoutineType
    {
        Goal, // Represents "goal"
        Done  // Represents "done"
    }
}