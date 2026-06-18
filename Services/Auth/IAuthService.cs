using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Services
{
    public interface IAuthService
    {
        public Task<LoginResponse> Login(LoginRequest user);
    }
}