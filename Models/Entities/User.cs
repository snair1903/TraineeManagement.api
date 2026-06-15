
using System.ComponentModel.DataAnnotations;
namespace TraineeManagement.api.Models
{
    public class User
    {   
        public int Id { get; set; }


        public string Username { get; set; } = "";

        public string Email { get; set; } = "";

        public string PasswordHash { get; private set; } = "";

        public UserRole Role { get; set; }
         
        public DateTime CreatedDate { get; set; } 

        public DateTime UpdatedDate { get; set; } 

    }
}