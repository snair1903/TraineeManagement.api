using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;
using TraineeManagement.api.Services;

[ApiController]
[Route("/api/auth")]

public class AuthController : ControllerBase
{
     private IAuthService _authService;

     public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest user)
    {
            LoginResponse? loginResponse = await _authService.Login(user);
            if (loginResponse == null)
        {
            return Unauthorized("UserName or Password is incorrect");
        }
        return Ok(loginResponse);
        
    }

}
