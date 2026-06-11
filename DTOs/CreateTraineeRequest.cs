namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.Models;

public class CreateTraineeRequest
{ 
    [Required(ErrorMessage = "FirstName is required.")]
    [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters.")]
    public string FirstName { get; set; } = "";
    [Required(ErrorMessage = "LastName is required.")]
    [StringLength(50, ErrorMessage = "LastName cannot exceed 50 characters.")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Valid email is required")]
    public string Email { get; set; } = "";
    [Required]
    public string TechStack { get; set; } = "";
    // [Required]
    // [AllowedValues(["Active", "InActive", "Complete"], ErrorMessage = "Must be valid Status")]
    public TraineeStatus Status { get; set; } 
}