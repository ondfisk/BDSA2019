# Notes

## Web API Authentication

File->New Project (configure with same name as existing project)

Copy over app settings and user secret

Go to <https://portal.azure.com/> and modify the new app registration. 
Create a scope under *Expose an API*

- Application ID URI: api://[clientId]
- Scope: user_impersonation

```powershell
Install-Package Microsoft.AspNetCore.Authentication.AzureAD.UI
```

Update `Startup.cs`.

```csharp
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;

...

services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
        .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
{
    // The valid audiences are both the clientId (options.Audience) and api://{clientId}
    options.TokenValidationParameters.ValidAudiences = new[]
    {
        options.Audience,
        $"api://{options.Audience}"
    };
});
...

app.UseAuthentication();
```

Update controllers:

```csharp
[Authorize]
```

## Xamarin.Forms authentication

Go to <https://portal.azure.com/> and create a new app registration. 
Link it to the one from the web api under *API Permissions*.

```powershell
Install-Package Microsoft.Identity.Client
```
