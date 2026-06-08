using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;

using Microsoft.AspNetCore.Http.HttpResults;
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
        try
        {
            var res = _traineeService.GetAll();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Internal Server Error",
                details = ex.Message
            });
        }
    }

    [HttpGet("{Id:int}")]

    public IActionResult GetbyId(int Id)
    {
        try
        {
            TraineeResponse? traineeResponse = _traineeService.GetById(Id);
            return traineeResponse == null ? NotFound() : Ok(traineeResponse);
         }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Internal Server Error",
                details = ex.Message
            });
        }
    }

    [HttpPost]
    public IActionResult Create(CreateTraineeRequest trainee)
    {
        try
        {
            TraineeResponse traineeResponse = _traineeService.Create(trainee);
            return Ok(traineeResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Internal Server Error",
                details = ex.Message
            });
        }
    }

    [HttpPut("{Id:int}")]

    public IActionResult Update(int Id, UpdateTraineeRequest updateTraineeRequest)
    {
        try
        {
            TraineeResponse traineeResponse = _traineeService.Update(Id, updateTraineeRequest);
            if (traineeResponse == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(traineeResponse);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Internal Server Error",
                details = ex.Message
            });
        }

    }

    [HttpDelete("{Id:int}")]

    public IActionResult Delete(int Id)
    {
        try
        {
            bool traineeResponse = _traineeService.Delete(Id);
            return traineeResponse == false ? NotFound() : StatusCode(204);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Internal Server Error",
                details = ex.Message
            });
        }
    }
}
