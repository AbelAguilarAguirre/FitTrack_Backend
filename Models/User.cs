using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("password")] // Maps to the `password` column in the database
        public string Password { get; set; }

        // Navigation property for routines
        public ICollection<Routine> Routines { get; set; } = new List<Routine>();

        // Constructor
        public User(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public User() { } // Parameterless constructor for EF Core
    }
}
    
    