using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class User
    {
        [Key]
        [Column("id")] // Maps to the `id` column in the database
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Column("username")] // Maps to the `username` column in the database
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("password")] // Maps to the `password` column in the database
        public string Password { get; set; } = string.Empty;        // Navigation property for routines
        [JsonIgnore]
        public ICollection<Routine> Routines { get; set; } = new List<Routine>();

        // Navigation property for activities
        [JsonIgnore]
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();// Constructor
        public User(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public User() { } // Parameterless constructor for EF Core
    }
}

