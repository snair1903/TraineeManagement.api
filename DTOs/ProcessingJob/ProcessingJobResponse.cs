using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs;

public class ProcessingJobResponse
{
    public int Id{get;set;}

    public ProcessingJobStatus Status {get;set;}

    public int Attempts{get;set;}
    
    public string ErrorSummary{get;set;} = string.Empty;

    public DateTime StartedTimestamp{get;set;}

    public DateTime CompletedTimestamp{get;set;} 

    public string CorrelationId{get;set;} = string.Empty;


    public ProcessingJobResponse(ProcessingJob processingJob)
    {
        Id = processingJob.Id;
        Status =processingJob.Status;
        Attempts = processingJob.Attempts;
        ErrorSummary = processingJob.ErrorSummary;
        StartedTimestamp = processingJob.StartedTimestamp;
        CompletedTimestamp = processingJob.CompletedTimestamp;
        CorrelationId = processingJob.CorrelationId;
    }
}