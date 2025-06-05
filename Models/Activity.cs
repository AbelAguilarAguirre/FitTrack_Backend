using System.ComponentModel.DataAnnotations;
namespace Backend.Models
{

    public class Activity
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }

        
        public ICollection<Routine> Routines { get; set; } = new List<Routine>();
        public Activity(string id, string Name, string Description)
        {
            Id = id;
            Name = Name;
            Description = Description;
            // Here you would typically save the user to a database
            // For this example, we are just creating the object
        }
    }
}
    