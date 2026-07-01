namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using Google.Protobuf.WellKnownTypes;
using TraineeManagement.api.Models;

public class CreateLearningTaskRequest
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string ExpectedTechStack { get; set; } = string.Empty;
    [Required]
    public LearnStatus Status { get; set; }
    [Required]
    public DateTime DueDate { get; set; }


}