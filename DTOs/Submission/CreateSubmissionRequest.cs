namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.Models;

public class CreateSubmissionRequest
{
    [Required(ErrorMessage ="Task Assignment Id is required")]
    public int TaskAssignmentId { get; set; }

    public string SubmissionUrl { get; set; } = "";

    public string Notes { get; set; } = "";

    public DateTime SubmissionDate { get; set; }
    public SubmissionStatus Status { get; set; }
}