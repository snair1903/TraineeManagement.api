using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Models
{
    public class Submission
    {   
        public int Id { get; set; }


        public int TaskAssignmentId { get; set; }

        public string SubmissionUrl { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public DateTime SubmissionDate { get; set; } 
        public SubmissionStatus Status { get; set; }
         
    public Submission(CreateSubmissionRequest submissionRequest)
        {
            TaskAssignmentId = submissionRequest.TaskAssignmentId;
            SubmissionUrl  = submissionRequest.SubmissionUrl;
            SubmissionDate = submissionRequest.SubmissionDate;
            Status = submissionRequest.Status;
        }


    public Submission(){}

    }
}