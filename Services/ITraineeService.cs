using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Services
{
    public interface ITraineeService
    {
        public List<TraineeResponse> GetAll();
        public TraineeResponse? GetById(int Id);
        public TraineeResponse Create(CreateTraineeRequest trainee);
        //Task<bool> UpdateAsync(int Id,Trainee trainee);
        //Task<bool> DeleteAsync(int Id);
    }
}