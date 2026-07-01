namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.Models;

public class CreateReviewRequest
{
    [Required]
    public int SubmissionId { get; set; }

    [Required]
    public int MentorId { get; set; }

    [Required]
    public string Feedback { get; set; } = string.Empty;

    public int Score { get; set; }

    public DateTime SubmissionDate { get; set; } = DateTime.Now;
    [Required]
    public ReviewStatus Status { get; set; }
}