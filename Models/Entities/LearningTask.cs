
namespace TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;


public class LearningTask
{
    public int Id { get; set; }

    public string Title { get; set; } = "";
    
    public string Description { get; set; } = "";

    public string ExpectedTechStack { get; set; } = "";
   
    public LearnStatus  Status { get; set; }
    public DateTime DueDate { get; set; } = DateTime.Now;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    public LearningTask(CreateLearningTaskRequest learningTask)
    {
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

    public LearningTask() { }
}