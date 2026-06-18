namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.Models;

public class CreateTaskAssignRequest
{
    [Required(ErrorMessage ="Trainee Id is required")]
    public int TraineeId { get; set; }
    [Required(ErrorMessage ="Trainee Id is required")]
    public int MentorId { get; set; }
    [Required(ErrorMessage ="Mentor Id is required")]
    public int LearningTaskId  { get; set; }
    [Required(ErrorMessage ="Assign Date is required")]
    public DateTime AssignedDate { get; set; } 
    [Required(ErrorMessage ="Due Date is required")]
    public DateTime DueDate { get; set; }
    [Required(ErrorMessage ="Status is required")]
    public TaskAssignStatus Status { get; set; }
    public string Remarks {get;set;} = "";
}