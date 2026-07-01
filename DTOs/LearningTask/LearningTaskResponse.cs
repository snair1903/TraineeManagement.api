namespace TraineeManagement.api.DTOs;

using System;
using TraineeManagement.api.Models;

public class LearningTaskResponse
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public string ExpectedTechStack { get; set; } = string.Empty;
   
    public LearnStatus  Status { get; set; }
    public DateTime DueDate { get; set; } = DateTime.Now;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;


    public LearningTaskResponse(LearningTask learningTask)
    {
        Id = learningTask.Id;
        Title = learningTask.Title;
        Description = learningTask.Description;
        ExpectedTechStack = learningTask.ExpectedTechStack;
        Status = learningTask.Status;
        DateTime d = DateTime.Now;
        DateTime timestmp = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        DueDate = learningTask.DueDate;
        CreatedDate = timestmp;
        UpdatedDate = timestmp;
    }

    public LearningTaskResponse() { }
    
    
}