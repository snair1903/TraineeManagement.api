using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Services
{
    public interface ISubmissionService
    {
        public Task<List<SubmissionResponse>> GetAll();
        public Task<SubmissionResponse> GetById(int Id);
        public Task<SubmissionResponse> Create(CreateSubmissionRequest Submission);
    }
}