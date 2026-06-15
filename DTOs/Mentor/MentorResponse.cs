namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Models;

public class MentorResponse
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
    public string Expertise { get; set; } = "";
   
    // [AllowedValues(["Active", "InActive", "Complete"], ErrorMessage = "Must be valid Status")]
    public MentorStatus Status { get; set; } 
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public MentorResponse(Mentor mentor)
    {
        Id = mentor.Id;
        FirstName = mentor.FirstName;
        LastName = mentor.LastName;
        Email = mentor.Email;
        Expertise = mentor.Expertise;
        Status = mentor.Status;
        CreatedDate = mentor.CreatedDate;
        UpdatedDate = mentor.UpdatedDate;
    }

    public MentorResponse(){}
}