namespace TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
public class LoginResponse
{
    public string Token { get; set; } = "";
 
    public DateTime ExpiresIn { get; set; } = DateTime.Now;

    public required  UserResponse  User{get; set;} 

}