namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.Models;

public class CreateTaskAssignRequest
{
    [Required]
    public int TraineeId { get; set; }
    [Required]
    public int MentorId { get; set; }
    [Required]
    public int LearningTaskId  { get; set; }
    [Required]
    public DateTime AssignedDate { get; set; } 
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public TaskAssignStatus Status { get; set; }
    public string Remarks {get;set;} = string.Empty;
}