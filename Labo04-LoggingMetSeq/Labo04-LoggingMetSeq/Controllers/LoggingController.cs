using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LoggingController : ControllerBase
{
    private readonly ILogger<LoggingController> _logger;
    public LoggingController(ILogger<LoggingController> logger)
    {
        _logger = logger;
    }

    [HttpGet("log-info")]
    public IActionResult LogInformation()
    {
        _logger.LogInformation("Dit is een informatiebericht.");
        return Ok("Information log created.");
    }

    [HttpGet("log-warning")]
    public IActionResult LogWarning()
    {
        _logger.LogWarning("Dit is een waarschuwing.");
        return Ok("Warning log created.");
    }

    [HttpGet("log-error")]
    public IActionResult LogError()
    {
        _logger.LogError("Dit is een foutmelding.");
        return Ok("Error log created.");
    }

    [HttpGet("log-exception")]
    public IActionResult LogException()
    {
        try
        {
            throw new Exception("Dit is een test-exceptie.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Er is een exceptie opgetreden.");
            return StatusCode(500, "Exception logged.");
        }
    }
}