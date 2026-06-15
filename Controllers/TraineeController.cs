using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;

using Microsoft.AspNetCore.Authorization;
using TraineeManagement.api.Models;
using TraineeManagement.api.Services;

[ApiController]
[Authorize]
[Route("/api/trainees")]
public class TraineeController : ControllerBase
{
    public ITraineeService _traineeService;

    public TraineeController(ITraineeService traineeService)
    {
        _traineeService = traineeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search, TraineeStatus? userStatus, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var res = await _traineeService.GetAll(pageNumber, pageSize, search, userStatus);
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

    public async Task<IActionResult> GetbyId(int Id)
    {
        try
        {
            TraineeResponse? traineeResponse = await _traineeService.GetById(Id);
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
    public async Task<IActionResult> Create(CreateTraineeRequest trainee)
    {
        TraineeResponse traineeResponse = await _traineeService.Create(trainee);
        return Created($"/api/learning-tasks/{traineeResponse.Id}", traineeResponse);
    }
    [HttpPut("{Id:int}")]

    public async Task<IActionResult> Update(int Id, UpdateTraineeRequest updateTraineeRequest)
    {
        TraineeResponse? traineeResponse = await _traineeService.Update(Id, updateTraineeRequest);
        return Ok(traineeResponse);

    }
    [HttpDelete("{Id:int}")]

    public async Task<IActionResult> Delete(int Id)
    {
        bool traineeResponse = await _traineeService.Delete(Id);
        return traineeResponse == false ? NotFound() : StatusCode(204);
    }
}
