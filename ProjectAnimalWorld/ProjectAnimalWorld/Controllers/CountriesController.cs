using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAnimalWorld.Models;
using ProjectAnimalWorld.Services.Interfaces;

namespace ProjectAnimalWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAll()
            => Ok(await _countriesService.GetAllCountries());

        [HttpGet("{id:int}/details")]
        public async Task<ActionResult<Country>> GetById(int id)
        {
            var country = await _countriesService.GetCountryById(id);
            return country == null ? NotFound() : Ok(country);
        }

        [HttpGet("continent/{continentId:int}")]
        public async Task<ActionResult<List<Country>>> GetByContinent(int continentId)
        {
            var list = await _countriesService.GetCountriesByContinent(continentId);
            return list.Any() ? Ok(list) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Country country)
        {
            await _countriesService.CreateCountry(country);
            return CreatedAtAction(nameof(GetById), new { id = country.Id }, country);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, Country country)
        {
            if (id != country.Id) return BadRequest();
            var updated = await _countriesService.UpdateCountry(id, country);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _countriesService.DeleteCountry(id);
            return NoContent();
        }
    }
}
