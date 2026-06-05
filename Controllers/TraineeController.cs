using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;
using TraineeManagement.api.Services;

[ApiController]
[Route("/api/trainee")]
public class TraineeController : ControllerBase
{
    public ITraineeService _traineeService;

    public TraineeController(ITraineeService traineeService)
    {
        _traineeService = traineeService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var res = _traineeService.GetAll();
        return Ok(res);
    }

    [HttpGet("{Id:int}")]

    public IActionResult GetbyId(int Id)
    {
        TraineeResponse? traineeResponse = _traineeService.GetById(Id);
        return traineeResponse == null? NotFound():Ok(traineeResponse);
    }

    [HttpPost]
    public IActionResult Create(CreateTraineeRequest trainee)
    {
        TraineeResponse traineeResponse = _traineeService.Create(trainee);
        return Ok(traineeResponse);
    }

    [HttpPut("{Id:int}")]

    public IActionResult Update(int Id,UpdateTraineeRequest updateTraineeRequest)
    {
        TraineeResponse traineeResponse = _traineeService.Update(Id,updateTraineeRequest);
        if(traineeResponse == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(traineeResponse);
        }
        
    }

    [HttpDelete("{Id:int}")]

    public IActionResult Delete(int Id)
    {
        bool traineeResponse = _traineeService.Delete(Id);
        return traineeResponse == false? NotFound():StatusCode(204);      
    }
}
