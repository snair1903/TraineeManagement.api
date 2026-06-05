using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Models;
namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/api/trainee")]
public class TraineeController : ControllerBase
{
    private static List<Trainee> trainees = new List<Trainee>
    {
        new Trainee{
            Id= 0,
            FirstName= "string",
            LastName= "string",
            Email= "string",
            TechStack= "string",
            Status= "string",
            CreatedDate= DateTime.Now,
            UpdatedDate= DateTime.Now
        }
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(trainees);
    }

    [HttpGet("{Id:int}")]

    public IActionResult GetbyId(int Id)
    {
        Trainee trainee = trainees.FirstOrDefault(t => t.Id == Id);
        return Ok(trainee);
    }

    [HttpPost]
    public IActionResult Create(Trainee trainee)
    {
        trainee.Id = trainees.Max(t => t.Id) + 1;
        trainee.CreatedDate = DateTime.Now;
        trainee.UpdatedDate = DateTime.Now;
        trainees.Add(trainee);
        return Ok(trainee);
    }
}
