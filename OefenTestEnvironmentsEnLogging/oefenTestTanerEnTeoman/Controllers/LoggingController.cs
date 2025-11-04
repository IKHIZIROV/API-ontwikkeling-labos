using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace oefenTestTanerEnTeoman.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;

        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        // 🟢 Voorbeeld: Booking log
        [HttpGet("bookings/book")]
        public IActionResult LogBooking()
        {
            _logger.LogInformation("Boeking uitgevoerd op {Time}", DateTime.Now);
            return Ok("Booking log created");
        }

        // 🟡 Voorbeeld: Cancel log
        [HttpGet("bookings/cancel")]
        public IActionResult LogCancel()
        {
            _logger.LogWarning("Boeking geannuleerd op {Time}", DateTime.Now);
            return Ok("Cancel log created");
        }

        // 🔴 Extra voorbeeld: Error log
        [HttpGet("bookings/error")]
        public IActionResult LogError()
        {
            _logger.LogError("Fout bij verwerken van boeking op {Time}", DateTime.Now);
            return Ok("Error log created");
        }
    }
}
