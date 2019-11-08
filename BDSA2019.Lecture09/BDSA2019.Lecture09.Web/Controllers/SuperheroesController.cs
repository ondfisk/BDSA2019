using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BDSA2019.Lecture09.Models;
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
    }
}
