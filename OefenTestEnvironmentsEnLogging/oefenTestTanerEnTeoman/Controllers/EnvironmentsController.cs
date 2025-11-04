using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace oefenTestTanerEnTeoman.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public EnvironmentsController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET /environment-message
        [HttpGet("environment-message")]
        public IActionResult GetEnvironmentMessage()
        {
            // ✨ EXACTE teksten zoals gevraagd in de opdracht
            string message = _env.EnvironmentName switch
            {
                "Development" => "Je draait de applicatie in de Development-omgeving.",
                "Staging" => "Je draait de applicatie in de Staging-omgeving.",
                "Production" => "Je draait de applicatie in de Production-omgeving.",
                _ => $"Je draait de applicatie in de {_env.EnvironmentName}-omgeving."
            };

            return Ok(new { message });
        }
    }
}
