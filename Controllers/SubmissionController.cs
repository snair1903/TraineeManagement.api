using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;
using Microsoft.AspNetCore.Authorization;
using TraineeManagement.api.Models;
using TraineeManagement.api.Services;


[ApiController]
[Authorize]
[Route("/api/submissions")]
public class SubmissionController : ControllerBase
{
    public ISubmissionService _SubmissionService;

    public SubmissionController(ISubmissionService SubmissionService)
    {
        _SubmissionService = SubmissionService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res =await _SubmissionService.GetAll();
            return Ok(res);
    }
    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetbyId(int Id)
    {
        SubmissionResponse? SubmissionResponse = await _SubmissionService.GetById(Id);
            return SubmissionResponse == null ? NotFound() : Ok(SubmissionResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSubmissionRequest Submission)
    {
        SubmissionResponse SubmissionResponse = await _SubmissionService.Create(Submission);
            return Ok(SubmissionResponse);
    }
    
}
