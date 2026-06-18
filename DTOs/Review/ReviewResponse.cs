
namespace TraineeManagement.api.DTOs;

using System;
using TraineeManagement.api.Models;

public class ReviewResponse
{
    public int Id { get; set; }

    public int SubmissionId { get; set; }

    public int MentorId { get; set; }


    public string Feedback { get; set; } = "";

    public int Score { get; set; }

    public DateTime SubmissionDate { get; set; } 
    public ReviewStatus Status { get; set; }

    public ReviewResponse(Review review)
    {
        Id = review.Id;
        SubmissionId = review.SubmissionId;
        Feedback = review.Feedback;
        Score = review.Score;
        SubmissionDate = review.SubmissionDate;
        Status = review.Status;
    }

    public ReviewResponse() { }


}