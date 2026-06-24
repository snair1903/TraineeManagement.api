using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs;

public class ProcessingJobResponse
{
    public int Id{get;set;}

    public ProcessingJobStatus Status {get;set;}

    public string Error{get;set;} = "";
    
    public string Summary{get;set;} = "";

    public DateTime StartedTimestamp{get;set;}

    public DateTime CompletedTimestamp{get;set;} 

    public string CorrelationId{get;set;} = "";


    public ProcessingJobResponse(ProcessingJob processingJob)
    {
        Id = processingJob.Id;
        Status =processingJob.Status;
        Error = processingJob.Error;
        Summary = processingJob.Summary;
        StartedTimestamp = processingJob.StartedTimestamp;
        CompletedTimestamp = processingJob.CompletedTimestamp;
        CorrelationId = processingJob.CorrelationId;
    }
}