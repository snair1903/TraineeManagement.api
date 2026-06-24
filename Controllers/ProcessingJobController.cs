using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;

using Microsoft.AspNetCore.Authorization;
using TraineeManagement.api.Services;

[ApiController]
[Authorize]
[Route("/api/processing-jobs")]
public class ProcessingJobController : ControllerBase
{
    public IProcessingJobService _ProcessingJobService;

    public ProcessingJobController(IProcessingJobService ProcessingJobService)
    {
        _ProcessingJobService = ProcessingJobService;
    }

    [HttpGet("{Id:int}")]

    public async Task<IActionResult> GetbyId(int Id)
    {
        try
        {
            ProcessingJobResponse? ProcessingJobResponse = await _ProcessingJobService.GetById(Id);
            return ProcessingJobResponse == null ? NotFound() : Ok(ProcessingJobResponse);
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
