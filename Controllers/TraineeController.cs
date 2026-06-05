using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Models;
namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TraineeController : ControllerBase
{
    private static List<Trainee> trainees = new List<Trainee>{};
    
    [HttpGet]
    public List<Trainee> GetAll()
    {
        return trainees;
    }
    
    [HttpGet("{Id}")]

    public Trainee GetbyId(int Id)
    {
        return trainees.FirstOrDefault(t=>t.Id==Id);
    }

    [HttpPost]
    public Trainee Create(Trainee trainee)
    {
        trainee.Id = trainees.Max(t =>t.Id)+1;
        trainees.Add(trainee);
        return trainee;
    }
}
