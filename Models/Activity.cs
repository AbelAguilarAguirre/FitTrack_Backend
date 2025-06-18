using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models
{

    public class Activity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // Foreign key to User

        [Required]
        [MaxLength(100)]
        public string name { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;        // Navigation properties
        [JsonIgnore]
        public User? User { get; set; } // Navigation property for User
        [JsonIgnore]
        public ICollection<Routine> Routines { get; set; } = new List<Routine>(); public Activity() { } // Parameterless constructor for deserialization

        public Activity(int id, int userId, string name, string description)
        {
            Id = id;
            UserId = userId;
            this.name = name;
            this.description = description;
            // Here you would typically save the user to a database
            // For this example, we are just creating the object
        }
    }
}
