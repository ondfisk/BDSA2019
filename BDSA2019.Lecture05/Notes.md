# Notes

## Extend constructor

```csharp
public SuperheroContext() { }

public SuperheroContext(DbContextOptions<SuperheroContext> options)
    : base(options) { }
```
