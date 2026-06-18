namespace TraineeManagement.api.Models;

using System.ComponentModel.DataAnnotations;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;

public class TaskAssignment
{
    [Key]
    public int Id { get; set; }
    public int TraineeId { get; set; }
    public int MentorId { get; set; }

    public int LearningTaskId  { get; set; }

    public DateTime AssignedDate { get; set; } = DateTime.Now;

    public DateTime DueDate { get; set; }

    public TaskAssignStatus Status { get; set; }
    public string Remarks {get;set;} = "";

    public TaskAssignment(CreateTaskAssignRequest taskAssign)
    {
        TraineeId = taskAssign.TraineeId;
        MentorId = taskAssign.MentorId;
        LearningTaskId = taskAssign.LearningTaskId;
        AssignedDate = taskAssign.AssignedDate;
        DueDate = taskAssign.DueDate;
        Status = taskAssign.Status;
        Remarks = taskAssign.Remarks;

    }

    public TaskAssignment() { }
}