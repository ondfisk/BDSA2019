# Notes

## HTTPS development

[Source](https://www.hanselman.com/blog/DevelopingLocallyWithASPNETCoreUnderHTTPSSSLAndSelfSignedCerts.aspx)

```bash
# Trust
dotnet dev-certs https --trust

# Remove
dotnet dev-certs https --clean
```

## Controller action return types in ASP.NET Core web API

[Source](https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.0)

- Specific type
- `IActionResult`
- `ActionResult<T>`

## Secrets

[Source](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.0&tabs=windows)

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:SuperheroContext" "Server=(localdb)\MSSQLLocalDB;Database=BDSA2019.Lecture09;Trusted_Connection=True;MultipleActiveResultSets=true"

dotnet ef migrations add InitialMigrations --project .\BDSA2019.Lecture09.Entities\ --startup-project .\BDSA2019.Lecture09.Web\

dotnet ef database update --project .\BDSA2019.Lecture09.Entities\ --startup-project .\BDSA2019.Lecture09.Web\
```

## Register Services

```csharp
public void ConfigureServices(IServiceCollection services)
{
    ...

    services.AddDbContext<SuperheroContext>(o => o.UseSqlServer(Configuration.GetConnectionString("SuperheroContext")));
    services.AddScoped<ISuperheroContext, SuperheroContext>();
    services.AddScoped<ISuperheroRepository, SuperheroRepository>();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...

    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetService<SuperheroContext>();
        context.Database.Migrate();
    }
}
```

## Swagger

[Source](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.0&tabs=visual-studio)

```bash
dotnet add package Swashbuckle.AspNetCore --version 5.0.0-rc4
```

```csharp
using Microsoft.OpenApi.Models;

public void ConfigureServices(IServiceCollection services)
{
    ...

    // Register the Swagger generator, defining 1 or more Swagger documents
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });
}

public void Configure(IApplicationBuilder app)
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });

    ...
}
```

```csharp
[Fact]
public async Task Get_returns_result_from_repository()
{
    var superheroes = new List<SuperheroListDTO>();

    var repository = new Mock<ISuperheroRepository>();
    repository.Setup(s => s.ReadAsync()).ReturnsAsync(superheroes);

    var logger = new Mock<ILogger<SuperheroesController>>();

    var controller = new SuperheroesController(repository.Object, logger.Object);

    var actual = await controller.Get();

    Assert.Equal(superheroes, actual);
}

[Fact]
public async Task Get_given_id_returns_result_from_repository()
{
    var superhero = new SuperheroDetailsDTO();

    var repository = new Mock<ISuperheroRepository>();
    repository.Setup(s => s.ReadAsync(42)).ReturnsAsync(superhero);

    var logger = new Mock<ILogger<SuperheroesController>>();

    var controller = new SuperheroesController(repository.Object, logger.Object);

    var actual = await controller.Get(42);

    Assert.Equal(superhero, actual.Value);
}

[Fact]
public async Task Post_returns_CreatedAtAction_with_id()
{
    var superhero = new SuperheroUpdateDTO();

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
    var superhero = new SuperheroUpdateDTO();
    var repository = new Mock<ISuperheroRepository>();
    repository.Setup(s => s.UpdateAsync(superhero)).ReturnsAsync(response);

    var logger = new Mock<ILogger<SuperheroesController>>();

    var controller = new SuperheroesController(repository.Object, logger.Object);

    var actual = await controller.Put(superhero);

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

[HttpGet]
public async Task<IEnumerable<SuperheroListDTO>> Get()
{
    return await _repository.ReadAsync();
}

[HttpGet]
public async Task<ActionResult<SuperheroDetailsDTO>> Get(int id)
{
    return await _repository.ReadAsync(id);
}

[HttpPut]
public async Task<IActionResult> Post(SuperheroCreateDTO superhero)
{
    var (_, id) = await _repository.CreateAsync(superhero);

    return CreatedAtAction("Get", new { id }, null);
}

[HttpPut]
public async Task<IActionResult> Put(SuperheroUpdateDTO superhero)
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

[HttpPut]
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
```