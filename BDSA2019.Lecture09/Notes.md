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
dotnet user-secrets set "ConnectionStrings:SuperheroDB" "Server=(localdb)\MSSQLLocalDB;Database=Superheroes;Trusted_Connection=True;MultipleActiveResultSets=true"
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