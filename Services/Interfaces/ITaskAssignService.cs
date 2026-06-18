using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Services
{
    public interface ITaskAssignService
    {
        public Task<List<TaskAssignResponse>> GetAll();
        public Task<TaskAssignResponse> GetById(int Id);
        public Task<TaskAssignResponse> Create(CreateTaskAssignRequest TaskAssign);
        public Task<TaskAssignResponse> Update(int Id,TaskAssignStatus status);
    }
}