using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Services
{
    public interface ITraineeService
    {
        public Task<PagedResponse<TraineeResponse>> GetAll(int pageNumber, int pageSize,string? search,TraineeStatus? userStatus);
        public Task<TraineeResponse?> GetById(int Id);
        public Task<TraineeResponse> Create(CreateTraineeRequest trainee);
        public Task<TraineeResponse?> Update(int Id,UpdateTraineeRequest trainee);
        public Task<bool> Delete(int Id);
    }
}