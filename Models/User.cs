
using System.ComponentModel.DataAnnotations;
namespace TraineeManagement.api.Models
{
    public class User
    {   
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Valid email is required")]
        public string Email { get; set; } = "";

        [Required]
        public string PasswordHash { get; private set; } = "";

        [Required]
        [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid user Role.")]

        public UserRole Role { get; set; }
         
        public DateTime CreatedDate { get; set; } 

        public DateTime UpdatedDate { get; set; } 

    }
}