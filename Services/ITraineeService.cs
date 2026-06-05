using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Services
{
    public interface ITraineeService
    {
        public List<TraineeResponse> GetAll();
        public TraineeResponse? GetById(int Id);
        public TraineeResponse Create(CreateTraineeRequest trainee);
        public TraineeResponse Update(int Id,UpdateTraineeRequest trainee);
        public bool Delete(int Id);
    }
}