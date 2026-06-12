using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Services
{
    public interface IMentorService
    {
        public Task<PagedResponse<MentorResponse>> GetAll(int pageNumber, int pageSize,string? search,MentorStatus? userStatus);
        public Task<MentorResponse?> GetById(int Id);
        public Task<MentorResponse> Create(CreateMentorRequest Mentor);
        public Task<MentorResponse?> Update(int Id,UpdateMentorRequest Mentor);
        public Task<bool> Delete(int Id);
    }
}