using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

public class SubmissionService : ISubmissionService
{
    private readonly AppDbContext _SubmissionContext;
    private bool _redisAvailable = true;
    private readonly IDistributedCache _cache;

    private readonly ILogger<SubmissionService> _logger;
    public SubmissionService(AppDbContext context, ILogger<SubmissionService> logger, IDistributedCache cache)
    {
        _SubmissionContext = context;
        _logger = logger;
        _cache = cache;
    }

    public async Task<List<SubmissionResponse>> GetAll()
    {
        return await _SubmissionContext.Submissions.Select(t => new SubmissionResponse(t)).ToListAsync();
    }


    public async Task<SubmissionResponse> GetById(int Id)
    {
        string cacheKey = $"Submission_{Id}";
        string? cachedData = null;

        try
        {
            cachedData = await _cache.GetStringAsync(cacheKey);
            _redisAvailable = true;
        }
        catch
        {
            _redisAvailable = false;
        }

        // Console.WriteLine("{0}", cachedData);

        if (cachedData != null)
        {
            _logger.LogInformation("Cache hit for product {Id}", Id);
            return JsonSerializer.Deserialize<SubmissionResponse>(cachedData)!;
        }

        _logger.LogInformation("Cache miss for Id {Id}", Id);
        var Submission = await _SubmissionContext.Submissions.FindAsync(Id);
        if (Submission == null)
        {
            _logger.LogInformation("Get Failure: No Submission found at id{}", Id);
            throw new NotFoundException("Submission not found at Id" + Id);
        }
        _logger.LogInformation("Get Success: Submissionment found at id {}", Id);
        var res = new SubmissionResponse(Submission);
        var serialized = JsonSerializer.Serialize(res);

        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };

        if(_redisAvailable)
        {
            await _cache.SetStringAsync(cacheKey, serialized, cacheOptions);
        }
        return res;
    }

    public async Task<SubmissionResponse> Create(CreateSubmissionRequest Submission)
    {
        if (await _SubmissionContext.TaskAssignments.FindAsync(Submission.TaskAssignmentId) == null) { throw new NotFoundException("No Task Assignments Found with Id " + Submission.TaskAssignmentId); }
        var nt = new Submission(Submission);
        _SubmissionContext.Submissions.Add(nt);
        await _SubmissionContext.SaveChangesAsync();
        _logger.LogInformation("Create Success: Submission id {}", nt.Id);
        return new SubmissionResponse(nt);
    }


}