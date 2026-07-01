
using System.ComponentModel.DataAnnotations;
namespace TraineeManagement.api.Models
{
    public class User
    {   
        public int Id { get; set; }


        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public UserRole Role { get; set; }
         
        public DateTime CreatedDate { get; set; } 

        public DateTime UpdatedDate { get; set; } 

    }
}