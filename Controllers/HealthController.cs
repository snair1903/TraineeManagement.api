using Microsoft.AspNetCore.Mvc;
namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/api/health")]
public class HealthCheckController : ControllerBase
{
    
    [HttpGet(Name = "GetHealth")]
    public IActionResult Get()
    {
        var res = new
        {
            status = "running",
            application = "Trainee Management API",
            timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss")
        };

        return Ok(res);
    }
}
