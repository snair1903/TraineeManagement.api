
namespace TraineeManagement.api.DTOs;
using System;
using TraineeManagement.api.Models;

public class SubmissionResponse
{
    public int Id { get; set; }

    public int TaskAssignmentId { get; set; }

    public string SubmissionUrl { get; set; } = string.Empty;

    public string Notes { get;set; } = string.Empty;

    public DateTime SubmissionDate { get; set; }
    public SubmissionStatus Status { get; set; }


    public SubmissionResponse(Submission submission)
    {
        Id =    submission.Id;
        TaskAssignmentId = submission.TaskAssignmentId;
        SubmissionUrl = submission.SubmissionUrl;
        Notes = submission.Notes;
        SubmissionDate = submission.SubmissionDate;
        Status = submission.Status;
    }

    public SubmissionResponse() { }
    
    
}