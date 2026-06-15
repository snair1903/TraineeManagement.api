using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;


public class ReviewService : IReviewService
{
    private readonly AppDbContext _ReviewContext;

    private readonly ILogger<ReviewService>  _logger;
    public ReviewService(AppDbContext context,ILogger<ReviewService> logger)
    {
        _ReviewContext = context;
        _logger = logger;
    }

    public async Task<List<ReviewResponse>> GetAll()
    {
        return  await _ReviewContext.Reviews.Select(t=>new ReviewResponse(t)).AsNoTracking().ToListAsync();
    }


    public async Task<ReviewResponse> GetById(int Id)
    {
        var Review = _ReviewContext.Reviews.FirstOrDefault(t => t.Id == Id);
        if (Review == null)
        {
            _logger.LogInformation("Get Failure: No Review found at id{}",Id);
            throw new NotFoundException("Review found at Id"+Id);
        }
        _logger.LogInformation("Get Success: Reviewment found at id {}",Id);
        return new ReviewResponse(Review);
    }

    public async Task<ReviewResponse> Create(CreateReviewRequest Review)
    {
        if(await _ReviewContext.Submissions.FirstOrDefaultAsync(t => t.Id == Review.SubmissionId)==null){throw new NotFoundException("No Submission Found with Id "+Review.SubmissionId);}
        if(await _ReviewContext.Mentors.FirstOrDefaultAsync(t => t.Id == Review.MentorId)==null){throw new NotFoundException("No Submission Found with Id "+Review.MentorId);}
        var nt = new Review(Review);
        _ReviewContext.Reviews.Add(nt);
        await _ReviewContext.SaveChangesAsync();
        _logger.LogInformation("Create Success: Review id {}",nt.Id);
        return new ReviewResponse(nt);
    }

    
}