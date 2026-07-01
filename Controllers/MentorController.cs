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

        var res = await _MentorService.GetAll(pageNumber, pageSize, search, userStatus);
        return Ok(res);

    }
    [HttpGet("{Id:int}")]

    public async Task<IActionResult> GetbyId(int Id)
    {

        MentorResponse? MentorResponse = await _MentorService.GetById(Id);
        return MentorResponse == null ? NotFound() : Ok(MentorResponse);


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

        MentorResponse? MentorResponse = await _MentorService.Update(Id, updateMentorRequest);
        return Ok(MentorResponse);


    }
    [HttpDelete("{Id:int}")]

    public async Task<IActionResult> Delete(int Id)
    {
        bool MentorResponse = await _MentorService.Delete(Id);
        return MentorResponse == false ? NotFound() : StatusCode(204);
    }
}
