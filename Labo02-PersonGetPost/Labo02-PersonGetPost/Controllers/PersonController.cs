using Labo02_PersonGetPost.Models;
using Labo02_PersonGetPost.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Labo02_PersonGetPost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonController()
        {
            _personService = new PersonService();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _personService.GetPerson(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson(Person item)
        {
            await _personService.CreatePerson(item);

            return CreatedAtAction(nameof(GetPerson), new { id = item.Id }, item);
        }
    }
}
