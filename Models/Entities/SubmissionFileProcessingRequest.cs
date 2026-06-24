namespace TraineeManagement.api.Models
{
    public class SubmissionFileProcessingRequest
    {   
        public string MessageId { get; set; } = string.Empty;

        public string CorrelationId { get; set; } = string.Empty;

        public int SubmissionId { get; set; }
        
        public int FileId { get; set; }
        
        public DateTime RequestedAt { get; set; } = DateTime.Now;

        public int ContractVersion { get; set; }
    }
}