using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA2019.Lecture09.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BDSA2019.Lecture09.Models.Response;

namespace BDSA2019.Lecture09.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuperheroesController : ControllerBase
    {
        private readonly ISuperheroRepository _repository;
        private readonly ILogger<SuperheroesController> _logger;

        public SuperheroesController(ISuperheroRepository repository, ILogger<SuperheroesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperheroListDTO>>> Get()
        {
            return (await _repository.ReadAsync()).ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SuperheroDetailsDTO>> Get(int id)
        {
            var superhero = await _repository.ReadAsync(id);

            if (superhero == null)
            {
                return NotFound();
            }

            return superhero;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CreatedAtActionResult> Post([FromBody]SuperheroCreateDTO superhero)
        {
            var (_, id) = await _repository.CreateAsync(superhero);

            return CreatedAtAction("Get", new { id }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody]SuperheroUpdateDTO superhero)
        {
            var response = await _repository.UpdateAsync(superhero);

            switch (response)
            {
                case Updated:
                    return NoContent();
                case BDSA2019.Lecture09.Models.Response.NotFound:
                    return NotFound();
                default:
                    throw new NotSupportedException(); // <- can't happen
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _repository.DeleteAsync(id);

            switch (response)
            {
                case Deleted:
                    return NoContent();
                case BDSA2019.Lecture09.Models.Response.NotFound:
                    return NotFound();
                default:
                    throw new NotSupportedException(); // <- can't happen
            }
        }
    }
}
