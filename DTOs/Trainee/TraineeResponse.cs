namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Models;

public class TraineeResponse
{
     public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    
    public string Email { get; set; } = string.Empty;
    public string TechStack { get; set; } = string.Empty;
   
    // [AllowedValues(["Active", "InActive", "Complete"], ErrorMessage = "Must be valid Status")]
    public TraineeStatus Status { get; set; } 
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public TraineeResponse(Trainee trainee)
    {
        Id = trainee.Id;
        FirstName = trainee.FirstName;
        LastName = trainee.LastName;
        Email = trainee.Email;
        TechStack = trainee.TechStack;
        Status = trainee.Status;
        CreatedDate = trainee.CreatedDate;
        UpdatedDate = trainee.UpdatedDate;
    }

    public TraineeResponse(){}
}