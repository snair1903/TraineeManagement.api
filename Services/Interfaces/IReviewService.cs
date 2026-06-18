using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Services
{
    public interface IReviewService
    {
        public Task<List<ReviewResponse>> GetAll();
        public Task<ReviewResponse> GetById(int Id);
        public Task<ReviewResponse> Create(CreateReviewRequest Submission);
    }
}