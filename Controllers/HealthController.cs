using Microsoft.AspNetCore.Mvc;
namespace TraineeManagement.api.Controllers;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

[ApiController]
[Route("/api/health")]
public class HealthCheckController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    public HealthCheckController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet("ready")]
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


    [HttpGet]
    public async Task GetAsync()
    {
        var report = await _healthCheckService.CheckHealthAsync();

        Response.StatusCode = report.Status == HealthStatus.Healthy
            ? StatusCodes.Status200OK
            : StatusCodes.Status503ServiceUnavailable;

        await UIResponseWriter.WriteHealthCheckUIResponse(HttpContext, report);
    }
}
