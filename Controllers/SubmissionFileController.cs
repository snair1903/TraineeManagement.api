using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;
using TraineeManagement.api.Data;
using TraineeManagement.api.Models;
using TraineeManagement.api.Services;


[ApiController]
[Authorize]
[Route("/api/submission-files")]
public class SubmissionFileController : ControllerBase
{
    public IFileStorageService _SubmissionFileService;
    public AppDbContext _SubmissionFileContext;

    public SubmissionFileController(IFileStorageService SubmissionFileService,AppDbContext context)
    {
        _SubmissionFileService = SubmissionFileService;
        _SubmissionFileContext = context;
    }
    
    [HttpGet("{Id}/download")]
    public async Task<IActionResult> Download(int Id)
    {   
        FileStream submissionFile = await _SubmissionFileService.OpenReadAsync(Id);
        string filename = Path.GetFileName(submissionFile.Name);
        string ext = Path.GetExtension(filename);
        return File(submissionFile,$"application/{ext.Substring(1)}",filename);
    }

    
    [HttpDelete("{Id}/download")]
    public async Task<IActionResult> Delete(int Id)
    {
        await _SubmissionFileService.DeleteAsync(Id);
        return NoContent();
    }

    // [HttpPost]
   

    
}
