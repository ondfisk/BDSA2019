using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA2019.Lecture11.Models;
using BDSA2019.Lecture11.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BDSA2019.Lecture11.Models.Response;

namespace BDSA2019.Lecture11.Web.Controllers
{
    [ApiController]
    [Authorize]
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

            return response switch
            {
                Updated => NoContent(),
                Lecture11.Models.Response.NotFound => NotFound(),
                _ => throw new NotSupportedException(), // <- can't happen
            };
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _repository.DeleteAsync(id);

            return response switch
            {
                Deleted => NoContent(),
                Lecture11.Models.Response.NotFound => NotFound(),
                _ => throw new NotSupportedException(), // <- can't happen
            };
        }
    }
}
