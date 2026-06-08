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
            Id = t.Id,
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
            Id = Id,
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
        var id = 0;
        if (trainees.Count() != 0)
        {
             id = trainees.Max(t => t.Id) + 1;
        }
        var nt = new Trainee
        {
            Id = id,
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
            Id = nt.Id,
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

    public TraineeResponse? Update(int Id,UpdateTraineeRequest updateTraineeRequest)
    {
        var trainee = trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee == null)
        {
            return null;
        }
        trainee.FirstName = updateTraineeRequest.FirstName;
        trainee.LastName = updateTraineeRequest.LastName;
        trainee.TechStack = updateTraineeRequest.TechStack;
        trainee.Email = updateTraineeRequest.Email;
        trainee.UpdatedDate = updateTraineeRequest.UpdatedDate;
        trainee.Status =  updateTraineeRequest.Status;

        var res = new TraineeResponse
        {
            Id = trainee.Id,
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            TechStack = trainee.TechStack,
            Email = trainee.Email,
            Status = trainee.Status,
            CreatedDate = trainee.CreatedDate,
            UpdatedDate = trainee.UpdatedDate
            
        };
        return res;
    }

    public bool Delete(int Id)
    {   
        var trainee = trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee == null)
        {
            return false;
        }
        trainees.Remove(trainee);
        return true;
    }
}
