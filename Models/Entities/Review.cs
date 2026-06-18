using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Models
{
    public class Review
    {   
        public int Id { get; set; }


        public int SubmissionId { get; set; }

        public int MentorId { get; set; }


        public string Feedback { get; set; } = "";

        public int Score { get;  set; }

        public DateTime SubmissionDate { get; set; } 
        public ReviewStatus Status { get; set; }
        
         
    public Review(CreateReviewRequest reviewRequest)
        {
            SubmissionId = reviewRequest.SubmissionId;
            MentorId = reviewRequest.MentorId;
            Feedback  = reviewRequest.Feedback;
            SubmissionDate = reviewRequest.SubmissionDate;
            Status = reviewRequest.Status;
        }


    public Review(){}

    }
}