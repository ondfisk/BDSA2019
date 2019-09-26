dotnet new sln
dotnet new classlib --output .\BDSA2019.Lecture05.Entities
dotnet new classlib --output .\BDSA2019.Lecture05.Models
dotnet new xunit --output .\BDSA2019.Lecture05.Models.Tests
dotnet new console --output .\BDSA2019.Lecture05.App

dotnet add .\BDSA2019.Lecture05.Models reference .\BDSA2019.Lecture05.Entities
dotnet add .\BDSA2019.Lecture05.Models.Tests reference .\BDSA2019.Lecture05.Models
dotnet add .\BDSA2019.Lecture05.App reference .\BDSA2019.Lecture05.Models

dotnet sln add .\BDSA2019.Lecture05.Entities
dotnet sln add .\BDSA2019.Lecture05.Models
dotnet sln add .\BDSA2019.Lecture05.Models.Tests
dotnet sln add .\BDSA2019.Lecture05.App

dotnet build
dotnet test

dotnet list package --outdated

dotnet add .\BDSA2019.Lecture05.Models.Tests package coverlet.collector
dotnet add .\BDSA2019.Lecture05.Models.Tests package Microsoft.NET.Test.Sdk
dotnet add .\BDSA2019.Lecture05.Models.Tests package xunit
dotnet add .\BDSA2019.Lecture05.Models.Tests package xunit.runner.visualstudio

dotnet list package --outdated

dotnet build
dotnet test
