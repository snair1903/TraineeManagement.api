using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;


public class TraineeService : ITraineeService
{
    private static List<Trainee> trainees = new List<Trainee>
    {
        new Trainee{
            Id= 0,
            FirstName= "string",
            LastName= "string",
            Email= "string",
            TechStack= "string",
            Status= "string",
            CreatedDate= DateTime.Now,
            UpdatedDate= DateTime.Now
        }
    };

    public List<TraineeResponse> GetAll()
    {
        var traineesDto = trainees.Select(t => new TraineeResponse
        {
            FirstName = t.FirstName,
            LastName = t.LastName,
            CreatedDate = t.CreatedDate,
            UpdatedDate = t.UpdatedDate,
            Status = t.Status,
            Email = t.Email,
            TechStack = t.TechStack
        }).ToList();
        return traineesDto;
    }


    public TraineeResponse? GetById(int Id)
    {
        var trainee = trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee == null)
        {
            return null;
        }
        var ntrainee = new TraineeResponse
        {
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            TechStack = trainee.TechStack,
            Email = trainee.Email,
            CreatedDate = trainee.CreatedDate,
            UpdatedDate = trainee.UpdatedDate,
            Status = trainee.Status,
        };
        return ntrainee;
    }

    public TraineeResponse Create(CreateTraineeRequest trainee)
    {
        var nt = new Trainee
        {
            Id = trainees.Max(t => t.Id) + 1,
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            TechStack = trainee.TechStack,
            Email = trainee.Email,
            Status = trainee.Status,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };
        trainees.Add(nt);
        var res = new TraineeResponse
        {
            FirstName = nt.FirstName,
            LastName = nt.LastName,
            TechStack = nt.TechStack,
            Email = nt.Email,
            Status = nt.Status,
            CreatedDate = nt.CreatedDate,
            UpdatedDate = nt.UpdatedDate
            
        };
        return res;
    }
}
