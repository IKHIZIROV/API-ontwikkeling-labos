using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TussentijdseToets_Ismail_Khizirov.Services;

namespace TussentijdseToets_Ismail_Khizirov.Controllers
{
    [Route("api/rentals")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly ILogger<RentalController> _logger;

        private readonly int _userId;

        public RentalController(ILogger<RentalController> logger)
        {
            _logger = logger;
            _userId++;
        }

        [HttpGet("rent")]
        public IActionResult LogRent()
        {
            _logger.LogInformation($"Rented out at { DateTime.Now} with User Id {_userId}");
            return Ok("Rent log created");
        }

        [HttpGet("cancel")]
        public IActionResult LogCancel()
        {
            _logger.LogWarning("Rent cancelled");
            return Ok("Cancel log created");
        }

        [HttpGet("extend")]
        public IActionResult LogExtend()
        {
            _logger.LogInformation("Rent extended");
            return Ok("Extend log created");
        }
    }
}
