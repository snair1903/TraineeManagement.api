using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;

using Microsoft.AspNetCore.Authorization;
using TraineeManagement.api.Models;
using TraineeManagement.api.Services;


[ApiController]
[Authorize]
[Route("/api/learning-tasks")]
public class LearningTaskController : ControllerBase
{
    public ILearningTaskService _LearningTaskService;

    public LearningTaskController(ILearningTaskService LearningTaskService)
    {
        _LearningTaskService = LearningTaskService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search,LearnStatus? userStatus,int pageNumber=1, int pageSize=10)
    {
        var res =await _LearningTaskService.GetAll(pageNumber, pageSize,search,userStatus);
            return Ok(res);
    }
    [HttpGet("{Id:int}")]

    public async Task<IActionResult> GetbyId([FromRoute]int Id)
    {
        LearningTaskResponse? LearningTaskResponse = await _LearningTaskService.GetById(Id);
            return LearningTaskResponse == null ? NotFound() : Ok(LearningTaskResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLearningTaskRequest LearningTask)
    {
        LearningTaskResponse LearningTaskResponse = await _LearningTaskService.Create(LearningTask);
            return Created($"/api/learning-tasks/{LearningTaskResponse.Id}",LearningTaskResponse);
    }
    [HttpPut("{Id:int}")]

    public async Task<IActionResult> Update(int Id, UpdateLearningTaskRequest updateLearningTaskRequest)
    {
        LearningTaskResponse? LearningTaskResponse = await _LearningTaskService.Update(Id, updateLearningTaskRequest);
        return Ok(LearningTaskResponse);

    }
    [HttpDelete("{Id:int}")]

    public async  Task<IActionResult> Delete(int Id)
    {
        bool LearningTaskResponse = await _LearningTaskService.Delete(Id);
            return LearningTaskResponse == false ? NotFound() : StatusCode(204);
    }
}
