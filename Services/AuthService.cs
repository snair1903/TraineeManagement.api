using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using TraineeManagement.api.Data;
using TraineeManagement.api.Utils;

public class AuthService : IAuthService
{
    private readonly AppDbContext _userContext;

    private readonly JwtService _jwtService;

    private readonly ILogger<AuthService>  _logger;
    public AuthService(AppDbContext context,JwtService jwtService,ILogger<AuthService> logger)
    {
        _userContext = context;
        _jwtService = jwtService;
        _logger = logger;
    }


    public async Task<LoginResponse?> Login(LoginRequest loginRequest)
    {
         if (string.IsNullOrWhiteSpace(loginRequest.UserName))
            throw new ArgumentException("UserName cannot be empty.", nameof(loginRequest.UserName));
        if (string.IsNullOrWhiteSpace(loginRequest.Password))
            throw new ArgumentException("Password cannot be empty.", nameof(loginRequest.Password));
        var user = _userContext.Users.FirstOrDefaultAsync(t => t.Username == loginRequest.UserName);
        if (user == null)
        {
             _logger.LogInformation("Login Failure: User not found");
            return null;
        }
       if (!PasswordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash))
        {
             _logger.LogInformation("Login Failure:In correct Id or password");
            return null;
        }
         var token = _jwtService.GenerateToken(1,user.Id, loginRequest.UserName,user.Role);

             _logger.LogInformation("Login Success");
            return new LoginResponse
            {
                Token = token,
                ExpiresIn = DateTime.Now.AddHours(1),
                User = new UserResponse (user)
                };
    }

}
