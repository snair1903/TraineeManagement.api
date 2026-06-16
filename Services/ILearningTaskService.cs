using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Services
{
    public interface ILearningTaskService
    {
        public Task<PagedResponse<LearningTaskResponse>> GetAll(int pageNumber, int pageSize,string? search,LearnStatus? userStatus);
        public Task<LearningTaskResponse?> GetById(int Id);
        public Task<LearningTaskResponse> Create(CreateLearningTaskRequest LearningTask);
        public Task<LearningTaskResponse?> Update(int Id,UpdateLearningTaskRequest LearningTask);
        public Task<bool> Delete(int Id);
    }
}