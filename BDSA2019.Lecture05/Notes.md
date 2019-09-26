# Notes

## Update packages

```bash
dotnet build
dotnet list package --outdated

dotnet tool list --global
dotnet tool update dotnet-ef --global
```

## Extend constructor

```csharp
public SuperheroContext() { }

public SuperheroContext(DbContextOptions<SuperheroContext> options)
    : base(options) { }
```

## Install Moq

```bash
dotnet add package Moq
```

## Test with Mocks

```csharp
var entity = new Superhero();
var context = new Mock<ISuperheroContext>();
context.Setup(c => c.Superheroes.Find(42)).Returns(entity);
var repository = new SuperheroRepository(context.Object);

var response = repository.Delete(42);

context.Verify(m => m.Superheroes.Remove(entity));
context.Verify(m => m.SaveChanges());
Assert.Equal(Deleted, response);
```

## Install InMemory Database in Models.Tests project

```bash
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

## Test with InMemory database

```csharp
var builder = new DbContextOptionsBuilder<SuperheroContext>().UseInMemoryDatabase(nameof(<method>));
var context = new SuperheroContext(builder.Options);
var repository = new SuperheroRepository(context);
```

## Test with Sqlite

```csharp
using var connection = new SqliteConnection("DataSource=:memory:");
connection.Open();
var builder = new DbContextOptionsBuilder<SuperheroContext>().UseSqlite(connection);
using var context = new SuperheroContext(builder.Options);
context.Database.EnsureCreated();
var repository = new SuperheroRepository(context);
```
