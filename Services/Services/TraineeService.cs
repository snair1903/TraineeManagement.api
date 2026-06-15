using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;


public class TraineeService : ITraineeService
{
    private readonly AppDbContext _traineeContext;

    private readonly ILogger<TraineeService>  _logger;
    public TraineeService(AppDbContext context,ILogger<TraineeService> logger)
    {
        _traineeContext = context;
        _logger = logger;
    }

    public async Task<PagedResponse<TraineeResponse>> GetAll(int pageNumber, int pageSize,string? search,TraineeStatus? userStatus)
    {
        var query = _traineeContext.Trainees.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(t => t.FirstName.ToLower().Contains(search) ||
            t.LastName.ToLower().Contains(search) || t.Email.ToLower().Contains(search) ||
            t.TechStack.ToLower().Contains(search));
        }

        if (userStatus != null)
        {
            query = query.Where(t => t.Status.ToString() == userStatus.ToString());
        }

        var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(t => new TraineeResponse(t)).ToListAsync();
        _logger.LogInformation("Get Success");
        return new PagedResponse<TraineeResponse>( data,pageNumber,pageSize, data.Count());
    }


    public async Task<TraineeResponse?> GetById(int Id)
    {
        var trainee = _traineeContext.Trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee == null)
        {
            _logger.LogInformation("Get Failure: No user found at id{}",Id);
            throw new NotFoundException($"Trainee not found at Id {Id}");
        }
        _logger.LogInformation("Get Success: user found at id {}",Id);
        return new TraineeResponse(trainee);
    }

    public async Task<TraineeResponse> Create(CreateTraineeRequest trainee)
    {

        var nt = new Trainee(trainee);
        _traineeContext.Trainees.Add(nt);
        await _traineeContext.SaveChangesAsync();
        _logger.LogInformation("Create Success: Trainee id {}",nt.Id);
        return new TraineeResponse(nt);
    }

    public async Task<TraineeResponse?> Update(int Id, UpdateTraineeRequest updateTraineeRequest)
    {
        var trainee = _traineeContext.Trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee == null)
        {
            _logger.LogInformation("Update Fail: Trainee not found at id {}",Id);
            throw new NotFoundException($"Trainee not found at {Id}");
        }
        trainee.FirstName = updateTraineeRequest.FirstName;
        trainee.LastName = updateTraineeRequest.LastName;
        trainee.TechStack = updateTraineeRequest.TechStack;
        trainee.Email = updateTraineeRequest.Email;
        trainee.UpdatedDate = updateTraineeRequest.UpdatedDate;
        trainee.Status = updateTraineeRequest.Status;

        await _traineeContext.SaveChangesAsync();
        _logger.LogInformation("Update Success: id {}",Id);
        return new TraineeResponse(trainee);
    }

    public async Task<bool> Delete(int Id)
    {
        var trainee = _traineeContext.Trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee == null)
        {
            _logger.LogInformation("Delete Error:Trainee not Found at id {}",Id);
            throw new NotFoundException($"Trainee not found at {Id}");
        }
        _traineeContext.Trainees.Remove(trainee);
        await _traineeContext.SaveChangesAsync();
        _logger.LogInformation("Delete Success: id{}",Id);
        return true;
    }
}
