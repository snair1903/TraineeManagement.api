using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TraineeManagement.api.Exceptions;
using TraineeManagement.api.Models;
using TraineeManagement.api.Services;


[ApiController]
[Authorize]
[Route("/api/submissions")]
public class SubmissionController : ControllerBase
{
    public ISubmissionService _SubmissionService;

    public IFileStorageService _SubmissionFileService;

    public SubmissionController(ISubmissionService SubmissionService, IFileStorageService fileStorageService)
    {
        _SubmissionService = SubmissionService;
        _SubmissionFileService = fileStorageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res = await _SubmissionService.GetAll();
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
        return Created($"/api/submissions/{SubmissionResponse.Id}", SubmissionResponse);
    }

    [RequestSizeLimit(10_485_760)] // Total request limit: 10 MB
    [RequestFormLimits(MultipartBodyLengthLimit = 10_485_760)]
    [HttpPost("{submissionId}/files")]
    public async Task<IActionResult> Save(int submissionId, CreateSubmissionFileRequest createSubmissionFileRequest)
    {

        int UploadedById = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        SubmissionFileResponse submissionFileResponse = await _SubmissionFileService.SaveAsync(createSubmissionFileRequest, UploadedById, submissionId);
        return Created($"/api/submissions/{submissionId}/files", submissionFileResponse);
    }




}
