// status, attempts, error summary, started/completed timestamps, and correlation ID.
namespace TraineeManagement.api.Models;

public class ProcessingJob
{
    public int Id{get;set;}

    public ProcessingJobStatus Status {get;set;}

    public string Error{get;set;} = "";
    
    public string Summary{get;set;} = "";

    public DateTime StartedTimestamp{get;set;}

    public DateTime CompletedTimestamp{get;set;}

    public string CorrelationId{get;set;} = "";
    
}