using System.Security.Policy;
using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs;

public class UserResponse
{
    public int Id {get; set;}

    public string UserName {get; set;} = string.Empty;

    public UserRole Role{get;set;}

    public UserResponse(User user)
    {
        Id = user.Id;
        UserName = user.Username;
        Role = user.Role;
    }

    public UserResponse(){}
}