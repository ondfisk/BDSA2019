# Notes

```csharp
var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Futurama;Integrated Security=True";

serviceCollection.AddScoped<ICharacterRepository>(_ => new AdoNetCharacterRepository(connectionString));

//serviceCollection.AddDbContext()
serviceCollection.AddScoped<ICharacterContext, CharacterContext>(_ => new CharacterContext(connectionString));
serviceCollection.AddScoped<ICharacterRepository, EntityFrameworkCharacterRepository>();

var container = IoCContainer.Container;

var repo = container.GetService<ICharacterRepository>();

var bridge = new Bridge(repo);

await bridge.PrintAll();
```
