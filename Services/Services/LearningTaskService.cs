using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;

public class LearningTaskService : ILearningTaskService
{
    private readonly AppDbContext _LearningTaskContext;

    private readonly ILogger<LearningTaskService>  _logger;
    public LearningTaskService(AppDbContext context,ILogger<LearningTaskService> logger)
    {
        _LearningTaskContext = context;
        _logger = logger;
    }

    public async Task<PagedResponse<LearningTaskResponse>> GetAll(int pageNumber, int pageSize,string? search,LearnStatus? userStatus)
    {
        var query = _LearningTaskContext.LearningTasks.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(t => t.Title.ToLower().Contains(search) ||
            t.Description.ToLower().Contains(search) ||
            t.ExpectedTechStack.ToLower().Contains(search));
        }

        if (userStatus != null)
        {
            query = query.Where(t => t.Status.ToString() == userStatus.ToString());
        }

        var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(t => new LearningTaskResponse(t)).ToListAsync();
        _logger.LogInformation("Get Success");
        return new PagedResponse<LearningTaskResponse>( data,pageNumber,pageSize, data.Count());
    }


    public async Task<LearningTaskResponse?> GetById(int Id)
{
    var LearningTask = await _LearningTaskContext.LearningTasks.FindAsync(Id);
    if (LearningTask == null)
    {
        _logger.LogInformation("Get Failure: No user found at id {Id}", Id);
        return null;
    }
    _logger.LogInformation("Get Success: user found at id {Id}", Id);
    return new LearningTaskResponse(LearningTask);
}
    public async Task<LearningTaskResponse> Create(CreateLearningTaskRequest LearningTask)
    {
        if (LearningTask.DueDate < DateTime.Now)
        {
            throw new BadRequestException("DueDate must be Greater than current Date");
        }
        var nt = new LearningTask(LearningTask);
        _LearningTaskContext.LearningTasks.Add(nt);
        await _LearningTaskContext.SaveChangesAsync();
        _logger.LogInformation("Create Success: LearningTask id {}",nt.Id);
        return new LearningTaskResponse(nt);
    }

    public async Task<LearningTaskResponse?> Update(int Id, UpdateLearningTaskRequest updateLearningTaskRequest)
    {
        if (updateLearningTaskRequest.DueDate < DateTime.Now)
        {
            throw new BadRequestException("DueDate must be Greater than current Date");
        }
        var LearningTask = _LearningTaskContext.LearningTasks.FirstOrDefault(t => t.Id == Id);
        if (LearningTask == null)
        {
            _logger.LogInformation("Update Fail: LearningTask not found at id {}",Id);
            throw new NotFoundException($"Learning Task not found at Id {Id}");
        }
        LearningTask.Title = updateLearningTaskRequest.Title;
        LearningTask.Description = updateLearningTaskRequest.Description;
        LearningTask.ExpectedTechStack = updateLearningTaskRequest.ExpectedTechStack;
        LearningTask.DueDate = updateLearningTaskRequest.DueDate;
        LearningTask.Status = updateLearningTaskRequest.Status;

        await _LearningTaskContext.SaveChangesAsync();
        _logger.LogInformation("Update Success: id {}",Id);
        return new LearningTaskResponse(LearningTask);
    }

    public async Task<bool> Delete(int Id)
    {
        var LearningTask = _LearningTaskContext.LearningTasks.FirstOrDefault(t => t.Id == Id);
        if (LearningTask == null)
        {
            _logger.LogInformation("Delete Error:LearningTask not Found at id {}",Id);
            return false;
        }
        _LearningTaskContext.LearningTasks.Remove(LearningTask);
        await _LearningTaskContext.SaveChangesAsync();
        _logger.LogInformation("Delete Success: id{}",Id);
        return true;
    }
}
