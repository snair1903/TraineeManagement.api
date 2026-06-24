using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;

public class ProcessingJobService : IProcessingJobService
{
    private readonly AppDbContext _ProcessingJobContext;

    private readonly ILogger<ProcessingJobService> _logger;


    public ProcessingJobService(AppDbContext context, ILogger<ProcessingJobService> logger )
    {
        _ProcessingJobContext = context;
        _logger = logger;
    }


    public async Task<ProcessingJobResponse?> GetById(int Id)
    { 
        var ProcessingJob = await _ProcessingJobContext.ProcessingJobs.FindAsync(Id);
        if (ProcessingJob == null)
        {
            _logger.LogInformation("Get Failure: No user found at id{}", Id);
            throw new NotFoundException($"ProcessingJob not found at Id {Id}");
        }
        _logger.LogInformation("Get Success: user found at id {}", Id);

        return new ProcessingJobResponse(ProcessingJob);
    }


}
