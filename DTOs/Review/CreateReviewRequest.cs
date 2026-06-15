namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.Models;

public class CreateReviewRequest
{
    [Required(ErrorMessage = "SubmissionId is Required")]
    public int SubmissionId { get; set; }

    [Required(ErrorMessage = "MentorId is Required")]
    public int MentorId { get; set; }

    [Required(ErrorMessage = "Feedback is Required")]
    public string Feedback { get; set; } = "";

    public int Score { get; set; }

    public DateTime SubmissionDate { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "SubmissionId is Required")]
    public ReviewStatus Status { get; set; }
}