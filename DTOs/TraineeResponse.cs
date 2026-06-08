namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Models;

public class TraineeResponse
{
     public int Id { get; set; }
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
    [Required]
    [AllowedValues(["Active", "InActive", "Complete"], ErrorMessage = "Must be valid Status")]
    public string Status { get; set; } = "";
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
}