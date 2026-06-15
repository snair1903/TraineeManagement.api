namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.Models;

public class UpdateLearningTaskRequest
{
    [Required(ErrorMessage = "Title is Required")]
    [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
    public string Title { get; set; } = "";
    [Required(ErrorMessage = "Description is Required")]
    [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
    public string Description { get; set; } = "";
    [Required(ErrorMessage = "ExpectedTechStack is Required")]
    [StringLength(50, ErrorMessage = "ExpectedTechStack cannot exceed 50 characters.")]
    public string ExpectedTechStack { get; set; } = "";
    [Required(ErrorMessage = "Status is Required")]
    public LearnStatus Status { get; set; }
    [Required(ErrorMessage = "DueDate is Required")]
    public DateTime DueDate { get; set; }


}