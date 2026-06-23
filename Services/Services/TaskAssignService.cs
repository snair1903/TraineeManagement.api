using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

public class TaskAssignService : ITaskAssignService
{
    private readonly AppDbContext _TaskAssignContext;

    private readonly ILogger<TaskAssignService> _logger;

    private readonly IDistributedCache _cache;

    public TaskAssignService(AppDbContext context, ILogger<TaskAssignService> logger, IDistributedCache cache)
    {
        _TaskAssignContext = context;
        _logger = logger;
        _cache = cache;
    }

    public async Task<List<TaskAssignResponse>> GetAll()
    {
        return await _TaskAssignContext.TaskAssignments.Select(t => new TaskAssignResponse(t)).AsNoTracking().ToListAsync();
    }


    public async Task<TaskAssignResponse> GetById(int Id)
    {
        string cacheKey = $"TaskAssignment_{Id}";
        string? cachedData = await _cache.GetStringAsync(cacheKey);

        // Console.WriteLine("{0}", cachedData);

        if (cachedData != null)
        {
            _logger.LogInformation("Cache hit for product {Id}", Id);
            return JsonSerializer.Deserialize<TaskAssignResponse>(cachedData)!;
        }

        _logger.LogInformation("Cache miss for Id {Id}", Id);
        var TaskAssign = await _TaskAssignContext.TaskAssignments.FindAsync(Id);
        if (TaskAssign == null)
        {
            _logger.LogInformation("Get Failure: No TaskAssignment found at id{}", Id);
            throw new NotFoundException("Task Assignment found at Id" + Id);
        }
        _logger.LogInformation("Get Success: TaskAssignment found at id {}", Id);
        var res = new TaskAssignResponse(TaskAssign);
        var serialized = JsonSerializer.Serialize(res);

        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };

        await _cache.SetStringAsync(cacheKey, serialized, cacheOptions);
        return res;
    }

    public async Task<TaskAssignResponse> Create(CreateTaskAssignRequest TaskAssign)
    {
        if (await _TaskAssignContext.Trainees.FirstOrDefaultAsync(t => t.Id == TaskAssign.TraineeId) == null) { throw new NotFoundException("No Trainee Found with Id " + TaskAssign.TraineeId); }
        if (await _TaskAssignContext.Mentors.FirstOrDefaultAsync(t => t.Id == TaskAssign.MentorId) == null) { throw new NotFoundException("No Mentor Found with Id " + TaskAssign.MentorId); }
        if (await _TaskAssignContext.LearningTasks.FirstOrDefaultAsync(t => t.Id == TaskAssign.LearningTaskId) == null) { throw new NotFoundException("No LearningTask Found with Id " + TaskAssign.LearningTaskId); }
        if (TaskAssign.DueDate < TaskAssign.AssignedDate) { throw new BadRequestException("Due Date cannot be before Assigned date"); }
        var nt = new TaskAssignment(TaskAssign);
        _TaskAssignContext.TaskAssignments.Add(nt);
        await _TaskAssignContext.SaveChangesAsync();
        _logger.LogInformation("Create Success: TaskAssign id {}", nt.Id);
        return new TaskAssignResponse(nt);
    }

    public async Task<TaskAssignResponse> Update(int Id, TaskAssignStatus status)
    {
        var TaskAssign = _TaskAssignContext.TaskAssignments.FirstOrDefault(t => t.Id == Id);
        if (TaskAssign == null)
        {
            _logger.LogInformation("Get Failure: No user found at id{}", Id);
            throw new NotFoundException("TaskAssignment Not Found at Id " + Id);
        }
        _logger.LogInformation("Get Success: user found at id {}", Id);
        TaskAssign.Status = status;
        await _TaskAssignContext.SaveChangesAsync();
        var serializedData = JsonSerializer.Serialize(new TaskAssignResponse(TaskAssign));

        // 2. Set expiration parameters
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };
        string cacheKey = $"Trainee_{Id}";

        // 3. Overwrite the key with the new value
        await _cache.SetStringAsync(cacheKey, serializedData, options);
        return new TaskAssignResponse(TaskAssign);

    }
}