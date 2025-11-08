using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAnimalWorld.Models;
using ProjectAnimalWorld.Services;

namespace ProjectAnimalWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsService _animalsService;

        public AnimalsController(IAnimalsService animalsService)
        {
            _animalsService = animalsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Animal>>> GetAll()
            => Ok(await _animalsService.GetAllAnimals());

        [HttpGet("{id:int}/details")]
        public async Task<ActionResult<Animal>> GetById(int id)
        {
            var animal = await _animalsService.GetAnimalById(id);
            return animal == null ? NotFound() : Ok(animal);
        }

        [HttpGet("country/{countryId:int}")]
        public async Task<ActionResult<List<Animal>>> GetByCountry(int countryId)
        {
            var list = await _animalsService.GetAnimalsByCountry(countryId);
            return list.Any() ? Ok(list) : NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Animal>>> SearchByName([FromQuery] string name)
        {
            var list = await _animalsService.SearchAnimalsByName(name);
            return list.Any() ? Ok(list) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Animal animal)
        {
            await _animalsService.CreateAnimal(animal);
            return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, Animal animal)

        {
            if (id != animal.Id) return BadRequest();
            var updated = await _animalsService.UpdateAnimal(id, animal);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _animalsService.DeleteAnimal(id);
            return NoContent();
        }
    }
}
