namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Models;
public class UpdateMentorRequest
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
    public string Expertise { get; set; } = string.Empty;

    public MentorStatus Status { get; set; } 
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}