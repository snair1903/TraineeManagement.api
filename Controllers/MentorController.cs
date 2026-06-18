using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;

using Microsoft.AspNetCore.Authorization;
using TraineeManagement.api.Models;
using TraineeManagement.api.Services;


[ApiController]
[Authorize]
[Route("/api/mentors")]
public class MentorController : ControllerBase
{
    public IMentorService _MentorService;

    public MentorController(IMentorService MentorService)
    {
        _MentorService = MentorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search, MentorStatus? userStatus, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var res = await _MentorService.GetAll(pageNumber, pageSize, search, userStatus);
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
            MentorResponse? MentorResponse = await _MentorService.GetById(Id);
            return MentorResponse == null ? NotFound() : Ok(MentorResponse);
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
    public async Task<IActionResult> Create(CreateMentorRequest Mentor)
    {
        MentorResponse MentorResponse = await _MentorService.Create(Mentor);
        return Created($"/api/mentors/{MentorResponse.Id}", MentorResponse);
    }
    [HttpPut("{Id:int}")]

    public async Task<IActionResult> Update(int Id, UpdateMentorRequest updateMentorRequest)
    {
        try
        {
            MentorResponse? MentorResponse = await _MentorService.Update(Id, updateMentorRequest);
            return Ok(MentorResponse);
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

    public async Task<IActionResult> Delete(int Id)
    {
        bool MentorResponse = await _MentorService.Delete(Id);
        return MentorResponse == false ? NotFound() : StatusCode(204);
    }
}
