using System.ComponentModel.DataAnnotations;
namespace Backend.Models
{

    public class User
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        

        public User(string id, string name, string passwordHash)
        {
            Id = id;
            Name = name;
            PasswordHash = passwordHash;

            // Here you would typically save the user to a database
            // For this example, we are just creating the object
        }
    }
}
    
    