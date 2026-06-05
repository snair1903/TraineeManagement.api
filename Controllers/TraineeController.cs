using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
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
        var traineesDto = trainees.Select(t => new TraineeResponse
        {
            FirstName = t.FirstName,
            LastName = t.LastName,
            CreatedDate = t.CreatedDate,
            UpdatedDate = t.UpdatedDate,
            Status = t.Status,
            Email = t.Email,
            TechStack = t.TechStack
        });
        return Ok(traineesDto);
    }

    [HttpGet("{Id:int}")]

    public IActionResult GetbyId(int Id)
    {
        var trainee = trainees.FirstOrDefault(t => t.Id == Id);
        if(trainee == null)
        {
            return NotFound();
        }
        var traineeDto = new TraineeResponse
        {
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            TechStack = trainee.TechStack,
            Email = trainee.Email,
            CreatedDate = trainee.CreatedDate,
            UpdatedDate = trainee.UpdatedDate,
            Status = trainee.Status,
        };
        return Ok(traineeDto);
    }

    [HttpPost]
    public IActionResult Create(Trainee trainee)
    {
        trainee.Id = trainees.Max(t => t.Id) + 1;
        trainee.CreatedDate = DateTime.Now;
        trainee.UpdatedDate = DateTime.Now;
        trainees.Add(trainee);
        CreateTraineeRequest Ctrainee = new CreateTraineeRequest
        {
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            Email = trainee.Email,
            Status = trainee.Status,
            TechStack = trainee.TechStack
        };
        return Ok(trainee);
    }
}
