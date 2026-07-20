namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.Models;

public class CreateSubmissionRequest
{
    [Required]
    public int TaskAssignmentId { get; set; }

    public string SubmissionUrl { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime SubmissionDate { get; set; }
    public SubmissionStatus Status { get; set; }
}