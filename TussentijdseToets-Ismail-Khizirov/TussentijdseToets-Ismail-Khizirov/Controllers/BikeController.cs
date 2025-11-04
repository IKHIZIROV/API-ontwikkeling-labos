using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TussentijdseToets_Ismail_Khizirov.Models;
using TussentijdseToets_Ismail_Khizirov.Services;

namespace TussentijdseToets_Ismail_Khizirov.Controllers
{
    [Route("api/bikes")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeService;

        public BikeController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Bike>>> GetBikes()
        {
            var bikes = await _bikeService.GetAllBikes();
            return Ok(bikes);
        }

        [HttpGet("{id:int}/details")]
        public async Task<ActionResult<Bike>> GetBikeDetails(int id)
        {
            var bike = await _bikeService.GetBikeById(id);
            if (bike == null)
            {
                return NotFound();
            }
            return Ok(bike);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Bike>> SearchBikeByTypeAndLocation([FromQuery] string type, [FromQuery] string location)
        {
            var bikes = await _bikeService.GetBikeByTypeAndLocation(type, location);
            if (bikes == null || !bikes.Any())
            {
                return NotFound();
            }
            return Ok(bikes);
        }
    }
}
