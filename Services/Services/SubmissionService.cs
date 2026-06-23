using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;


public class SubmissionService : ISubmissionService
{
    private readonly AppDbContext _SubmissionContext;

    private readonly ILogger<SubmissionService>  _logger;
    public SubmissionService(AppDbContext context,ILogger<SubmissionService> logger)
    {
        _SubmissionContext = context;
        _logger = logger;
    }

    public async Task<List<SubmissionResponse>> GetAll()
    {
        return  await _SubmissionContext.Submissions.Select(t=>new SubmissionResponse(t)).AsNoTracking().ToListAsync();
    }


    public async Task<SubmissionResponse> GetById(int Id)
    {
        var Submission = await _SubmissionContext.Submissions.FindAsync( Id);
        if (Submission == null)
        {
            _logger.LogInformation("Get Failure: No Submission found at id{}",Id);
            throw new NotFoundException("Submission not found at Id"+Id);
        }
        _logger.LogInformation("Get Success: Submissionment found at id {}",Id);
        return new SubmissionResponse(Submission);
    }

    public async Task<SubmissionResponse> Create(CreateSubmissionRequest Submission)
    {
        if(await _SubmissionContext.TaskAssignments.FindAsync(Submission.TaskAssignmentId)==null){throw new NotFoundException("No Task Assignments Found with Id "+Submission.TaskAssignmentId);}
        var nt = new Submission(Submission);
        _SubmissionContext.Submissions.Add(nt);
        await _SubmissionContext.SaveChangesAsync();
        _logger.LogInformation("Create Success: Submission id {}",nt.Id);
        return new SubmissionResponse(nt);
    }

    
}