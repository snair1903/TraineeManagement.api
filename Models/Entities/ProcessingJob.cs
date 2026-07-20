// status, attempts, error summary, started/completed timestamps, and correlation ID.
namespace TraineeManagement.api.Models;
using System.ComponentModel.DataAnnotations;
public class ProcessingJob
{
    public int Id{get;set;}

    public ProcessingJobStatus Status {get;set;}

    public int Attempts{get;set;}
    [MaxLength(50)]
    public string ErrorSummary{get;set;} = string.Empty;

    public DateTime StartedTimestamp{get;set;}

    public DateTime CompletedTimestamp{get;set;}

    public string CorrelationId{get;set;} = string.Empty;
    
}