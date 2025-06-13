using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Backend.Models
{

    public class Activity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        public string description { get; set; }

        
        public ICollection<Routine> Routines { get; set; } = new List<Routine>();
        public Activity(int id, string name, string description)
        {
            Id = id;
            name = name;
            description = description;
            // Here you would typically save the user to a database
            // For this example, we are just creating the object
        }
    }
}
    