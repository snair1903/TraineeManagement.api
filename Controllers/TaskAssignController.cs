using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;
using Microsoft.AspNetCore.Authorization;
using TraineeManagement.api.Models;
using TraineeManagement.api.Services;


[ApiController]
[Authorize]
[Route("/api/task-assignments")]
public class TaskAssignController : ControllerBase
{
    public ITaskAssignService _TaskAssignService;

    public TaskAssignController(ITaskAssignService TaskAssignService)
    {
        _TaskAssignService = TaskAssignService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res =await _TaskAssignService.GetAll();
            return Ok(res);
    }
    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetbyId(int Id)
    {
        TaskAssignResponse? TaskAssignResponse = await _TaskAssignService.GetById(Id);
            return TaskAssignResponse == null ? NotFound() : Ok(TaskAssignResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskAssignRequest TaskAssign)
    {
        TaskAssignResponse TaskAssignResponse = await _TaskAssignService.Create(TaskAssign);
            return Created($"/api/task-assignment/{TaskAssignResponse.Id}",TaskAssignResponse);
    }
    [HttpPut("{Id:int}")]

    public async Task<IActionResult> Update(int Id,TaskAssignStatus status)
    {
       TaskAssignResponse TaskAssignResponse = await _TaskAssignService.Update(Id,status);
       return Ok(TaskAssignResponse);

    }
    
}
