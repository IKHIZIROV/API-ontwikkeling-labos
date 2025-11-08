using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAnimalWorld.Models;
using ProjectAnimalWorld.Services;

namespace ProjectAnimalWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContinentsController : ControllerBase
    {
        private readonly IContinentsService _continentsService;

        public ContinentsController(IContinentsService continentsService)
        {
            _continentsService = continentsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Continent>>> GetAll()
            => Ok(await _continentsService.GetAllContinents());

        [HttpGet("{id:int}/details")]
        public async Task<ActionResult<Continent>> GetById(int id)
        {
            var item = await _continentsService.GetContinentById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Continent continent)
        {
            await _continentsService.CreateContinent(continent);
            return CreatedAtAction(nameof(GetById), new { id = continent.Id }, continent);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, Continent continent)
        {
            if (id != continent.Id) return BadRequest();
            var updated = await _continentsService.UpdateContinent(id, continent);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _continentsService.DeleteContinent(id);
            return NoContent();
        }
    }
}
