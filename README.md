# .NET 8 MongoDB Library

This .NET 8 class library provides comprehensive support for MongoDB operations through a well-defined repository pattern. The library facilitates the easy integration of MongoDB into .NET applications by providing a robust set of asynchronous CRUD operations and transaction support, streamlined through dependency injection.

## Features

- Asynchronous CRUD operations
- Advanced query capabilities with filters
- Support for MongoDB transactions
- Easy integration with .NET projects through dependency injection

## Getting Started

### Prerequisites

To use this library, you will need:
- .NET 8 SDK installed on your development machine
- MongoDB Server installed and running
- An IDE that supports .NET development (e.g., Visual Studio, Visual Studio Code)

### Installation

Follow these steps to incorporate the MongoDB library into your .NET project:

1. Clone the repository:

    ```bash
    git clone https://github.com/ssinanylmz/Library.Mongo.git
    ```

2. Include the library in your .NET solution:

    ```bash
    dotnet add reference /path/to/your/Library.Mongo.csproj
    ```

3. Restore dependencies:

    ```bash
    dotnet restore
    ```

### Configuration

Configure your application to use the MongoDB library by adding the MongoDB connection settings in your `appsettings.json`:

```json
{
    "MongoDbSettings": {
        "ConnectionString": "your_connection_string",
        "DatabaseName": "your_database_name"
    }
}
```
## Registering Services
Add MongoDB services to your application's service container in the Startup.cs or where you configure services:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMongoDbServices(Configuration);
}
```
## Usage
The MongoDB library offers a generic repository interface IMongoRepository<T> which you can use to perform database operations:

```csharp
public class YourDataService
{
    private readonly IMongoRepository<YourDocument> _repository;

    public YourDataService(IMongoRepository<YourDocument> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<YourDocument>> GetAllDocumentsAsync()
    {
        return await _repository.GetAllAsync();
    }

    // Implement other operations similarly
}
```

## Contributing
We welcome contributions from the community and value new ideas:

1. Fork the repository on GitHub.
2. Create a new branch for your feature (git checkout -b new-feature).
3. Commit your changes (git commit -am 'Add some feature').
4. Push the branch (git push origin new-feature).
5. Submit a pull request through the GitHub interface.

