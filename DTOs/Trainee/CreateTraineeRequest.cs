namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.Models;

public class CreateTraineeRequest
{ 
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string TechStack { get; set; } = string.Empty;
    // [Required]
    // [AllowedValues(["Active", "InActive", "Complete"], ErrorMessage = "Must be valid Status")]
    public TraineeStatus Status { get; set; } 
}