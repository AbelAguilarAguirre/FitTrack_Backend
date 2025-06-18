using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Routine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public int user_id { get; set; }

        [Required]
        public int activity_id { get; set; } // Renamed to follow PascalCase        [Required]
        public int value { get; set; }

        [Required]
        [MaxLength(20)]
        public string unit { get; set; }
        public int repetitions { get; set; }

        public double progress { get; set; } = 0; // Progress towards the goal (0 to value)

        public DateTime? Date { get; set; } // Changed to DateTime? for better type safety

        [Required]
        public RoutineType Type { get; set; } // Enum for type safety        // Add navigation properties
        public User User { get; set; } // Navigation property for User
        public Activity Activity { get; set; } // Navigation property for Activity

        // Default constructor for Entity Framework and JSON serialization
        public Routine() { }

        public Routine(int id, int user_id, int activity_id, int value, string unit, int repetitions = 0, double progress = 0, DateTime? date = null, RoutineType type = RoutineType.done)
        {
            this.Id = id;
            this.user_id = user_id;
            this.activity_id = activity_id;
            this.value = value;
            this.unit = unit;
            this.repetitions = repetitions;
            this.progress = progress;
            this.Date = date;
            this.Type = type;
        }
    }
    public enum RoutineType
    {
        goal = 0,  // Maps to 'goal' in database
        done = 1   // Maps to 'done' in database
    }
}