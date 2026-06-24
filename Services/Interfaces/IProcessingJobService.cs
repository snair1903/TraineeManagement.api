using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Services
{
    public interface IProcessingJobService
    {
        public Task<ProcessingJobResponse?> GetById(int Id);
    }
}