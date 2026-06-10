using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Data;
using TraineeManagement.api.Utils;
using Microsoft.AspNetCore.Identity;
public class AuthService : IAuthService
{
    private readonly AppDbContext _userContext;

    private readonly JwtService _jwtService;

    public AuthService(AppDbContext context,JwtService jwtService)
    {
        _userContext = context;
        _jwtService = jwtService;
    }


    public async Task<LoginResponse?> Login(LoginRequest loginRequest)
    {
         if (string.IsNullOrWhiteSpace(loginRequest.UserName))
            throw new ArgumentException("UserName cannot be empty.", nameof(loginRequest.UserName));
        if (string.IsNullOrWhiteSpace(loginRequest.Password))
            throw new ArgumentException("Password cannot be empty.", nameof(loginRequest.Password));
        var user = _userContext.Users.FirstOrDefault(t => t.Username == loginRequest.UserName);
        if (user == null)
        {
            return null;
        }
       if (!PasswordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash))
        {
            return null;
        }
         var token = _jwtService.GenerateToken(1, loginRequest.UserName);

            return new LoginResponse
            {
                Token = token,
                ExpiresIn = DateTime.Now.AddHours(1),
                User = new {
                    user.Id,
                    user.Username,
                    user.Role
                }
            };

    }

}