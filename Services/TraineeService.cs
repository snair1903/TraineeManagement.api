using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Data;


public class TraineeService : ITraineeService
{
    private readonly AppDbContext _traineeContext;

    public TraineeService(AppDbContext context)
    {
        _traineeContext = context;
    }

    public async Task<List<TraineeResponse>> GetAll(string? search)
    {
        var query = _traineeContext.Trainees.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(t => t.FirstName.ToLower().Contains(search) ||
            t.LastName.ToLower().Contains(search) || t.Email.ToLower().Contains(search) ||
            t.TechStack.ToLower().Contains(search));
        }

        return await query.Select(t => new TraineeResponse(t)).ToListAsync();
    }


    public async Task<TraineeResponse?> GetById(int Id)
    {
        var trainee = _traineeContext.Trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee == null)
        {
            return null;
        }
        return new TraineeResponse(trainee);
    }

    public async Task<TraineeResponse> Create(CreateTraineeRequest trainee)
    {

        var nt = new Trainee(trainee);
        _traineeContext.Trainees.Add(nt);
        await _traineeContext.SaveChangesAsync();
        return new TraineeResponse(nt);
    }

    public async Task<TraineeResponse?> Update(int Id, UpdateTraineeRequest updateTraineeRequest)
    {
        var trainee = _traineeContext.Trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee == null)
        {
            return null;
        }
        trainee.FirstName = updateTraineeRequest.FirstName;
        trainee.LastName = updateTraineeRequest.LastName;
        trainee.TechStack = updateTraineeRequest.TechStack;
        trainee.Email = updateTraineeRequest.Email;
        trainee.Status = updateTraineeRequest.Status;

        await _traineeContext.SaveChangesAsync();
        return new TraineeResponse(trainee);
    }

    public async Task<bool> Delete(int Id)
    {
        var trainee = _traineeContext.Trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee == null)
        {
            return false;
        }
        _traineeContext.Trainees.Remove(trainee);
        await _traineeContext.SaveChangesAsync();
        return true;
    }
}
