# Cars-Scraping

Welcome to the screen scraping project made with C#, Selenium and Entity Framework to save to database.

## Table of Contents

- Start here
  - [Path of drivers](#path-of-drivers)
  - [Default browser](#default-browser)
  - [Connections Strings](#connections-strings)
- Documentation
  - [Docs](#docs)

# Start Here

## Path of drivers

Path of your project that has to be changed before running the project.

As selenium uses browser access drivers, it has to change the storage location.

### How to find the location?

* Go to the CarsScraping.Infrastructure.Selenium project and look for a file called SeleniumSettings.json
* Change the DriverPath parameter to its correct path. According to the picture below.

```
{
  "SeleniumConfig": {
    "Browser": "Firefox",
    "DriverPath": "C:\\Users\\ronaldeived.silva\\source\\repos\\CarsScrape\\SeleniumDrivers",
    "IsHeadless": true,
    "NavigationTimeout": 120,
    "WaitElementTimeout": 5
  }
}
```

## Default browser

According to the image above, Firefox is already chosen as the default browser for this application, due to factors of greater performance and speed and also less bugs.

If you want to use other browsers, this application accepts it, type this way if you want to change it in the config:

* Chrome
* Firefox
* NewEdge

### And if there is an error?
If the browser driver compilation has a problem like the one below, it is recommended to follow the error step by step and download the latest driver version.


```
OpenQA.Selenium.DriverServiceNotFoundException: 'The file C:\Users\ronaldeived.silva\source\repos\CarsScrape\SeleniumDrivers\geckodriver.exe does not exist. The driver can be downloaded at https://github.com/mozilla/geckodriver/releases'
```

## Connections Strings
You will need to change your database password and username.
Don't worry, the database will be created automatically at runtime.


To find the class that should be changed, look for the CarsScraping.UI project, inside the context folder the CarsContext.cs file

Look for the OnConfiguring method and make the necessary change.

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }

```

# Documentation

## Docs

[Selenium Doc](https://www.selenium.dev/documentation/) Selenium is an umbrella project for a range of tools and libraries that enable and support the automation of web browsers. 

[Entity Framework](https://docs.microsoft.com/en-us/ef/) is a modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations. EF Core works with many databases, including SQL Database (on-premises and Azure), SQLite, MySQL, PostgreSQL, and Azure Cosmos DB.

[Dependency injection in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) .NET supports the dependency injection (DI) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies. Dependency injection in .NET is a built-in part of the framework, along with configuration, logging, and the options pattern.
