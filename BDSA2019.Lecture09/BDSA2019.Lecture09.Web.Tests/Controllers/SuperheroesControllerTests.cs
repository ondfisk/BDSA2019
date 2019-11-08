using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BDSA2019.Lecture09.Models;
using BDSA2019.Lecture09.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using static BDSA2019.Lecture09.Models.Response;

namespace BDSA2019.Lecture09.Web.Tests.Controllers
{
    public class SuperheroesControllerTests
    {
        [Fact]
        public async Task Get_returns_result_from_repository()
        {
            var superheroes = new List<SuperheroListDTO>();
            var repository = new Mock<ISuperheroRepository>();
            repository.Setup(m => m.ReadAsync()).ReturnsAsync(superheroes);

            var logger = new Mock<ILogger<SuperheroesController>>();

            var controller = new SuperheroesController(repository.Object, logger.Object);

            var result = await controller.Get();

            Assert.Equal(superheroes, result.Value);
        }

        [Fact]
        public async Task Get_id_returns_result_from_repository()
        {
            var superhero = new SuperheroDetailsDTO();
            var repository = new Mock<ISuperheroRepository>();
            repository.Setup(m => m.ReadAsync(42)).ReturnsAsync(superhero);

            var logger = new Mock<ILogger<SuperheroesController>>();

            var controller = new SuperheroesController(repository.Object, logger.Object);

            var result = await controller.Get(42);

            Assert.Equal(superhero, result.Value);
        }

        [Fact]
        public async Task Post_returns_CreatedAtAction_with_id()
        {
            var superhero = new SuperheroCreateDTO();

            var repository = new Mock<ISuperheroRepository>();
            repository.Setup(s => s.CreateAsync(superhero)).ReturnsAsync((Created, 42));

            var logger = new Mock<ILogger<SuperheroesController>>();

            var controller = new SuperheroesController(repository.Object, logger.Object);

            var actual = await controller.Post(superhero) as CreatedAtActionResult;

            Assert.Equal("Get", actual.ActionName);
            Assert.Equal(42, actual.RouteValues["id"]);
        }

        [Theory]
        [InlineData(Updated, typeof(NoContentResult))]
        [InlineData(NotFound, typeof(NotFoundResult))]
        public async Task Put_given_repository_returns_response_returns_returnType(Response response, Type returnType)
        {
            var superhero = new SuperheroUpdateDTO { Id = 12 };
            var repository = new Mock<ISuperheroRepository>();
            repository.Setup(s => s.UpdateAsync(superhero)).ReturnsAsync(response);

            var logger = new Mock<ILogger<SuperheroesController>>();

            var controller = new SuperheroesController(repository.Object, logger.Object);

            var actual = await controller.Put(12, superhero);

            Assert.IsType(returnType, actual);
        }

        [Theory]
        [InlineData(Deleted, typeof(NoContentResult))]
        [InlineData(NotFound, typeof(NotFoundResult))]
        public async Task Delete_given_repository_returns_response_returns_returnType(Response response, Type returnType)
        {
            var repository = new Mock<ISuperheroRepository>();
            repository.Setup(s => s.DeleteAsync(42)).ReturnsAsync(response);

            var logger = new Mock<ILogger<SuperheroesController>>();

            var controller = new SuperheroesController(repository.Object, logger.Object);

            var actual = await controller.Delete(42);

            Assert.IsType(returnType, actual);
        }
    }
}
