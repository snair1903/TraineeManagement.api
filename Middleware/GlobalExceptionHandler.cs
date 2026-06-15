
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Exceptions;
namespace TraineeManagement.api.Middleware;

public class GlobalExceptionhandler:IExceptionHandler
{
    private readonly ILogger<GlobalExceptionhandler> _logger;
    public GlobalExceptionhandler(ILogger<GlobalExceptionhandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var (statusCode,title)=exception switch
        {
          NotFoundException =>(StatusCodes.Status404NotFound,"Not Found"),
          BadRequestException =>(StatusCodes.Status400BadRequest,"Bad Request"),
          _ =>(StatusCodes.Status500InternalServerError,"InternalServer Error")  
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
        return true;
    }
}