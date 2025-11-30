**Project Overview**
- **Name:**: testBusiness (TechnicalExercice)
- **Solution layout:**: contains the web API project `TechnicalExercice` and tests `TechnicalExercice.Tests`.

**Repository Structure**
- **`TechnicalExercice/`**: ASP.NET Core minimal API project containing the application, API endpoints, domain models and EF DbContext.
	- **`API/`**: Minimal API endpoint definitions and extensions (`ClientEndPoint`, `EndpointExtensions`, DTOs like `AddProductRequest`).
	- **`Application/`**: Application services (use-case classes) such as `AddProductToBasket`, `CalculateTotalBasket`, `CreateClient`, `GetClients`.
	- **`Domain/`**: Domain models and interfaces.
		- **`Models/`**: `Client`, `Individual`, `Professional`, `Product`, `ShoppingBasket`, enums (`ClientType`, `TypeProduct`, `Currency`).
		- **`Interface/`**: Domain service contracts (`IclientService`, `IShoppingService`, `IProductService`).
	- **`Infrastructure/`**: Implementation of persistence and services.
		- **`Context/`**: `MyDbContext` (InMemory configured for tests/runtime convenience).
		- **`Services/`**: Implementations like `ClientService`, `ProductService`, `ShoppingService`.

- **`TechnicalExercice.Tests/`**: Test project with unit and integration tests using `xUnit`, `Moq` and `Microsoft.AspNetCore.Mvc.Testing` for host integration.

**How to build & run (local)**
- **Restore & build:**

	- `dotnet restore`
	- `dotnet build`

- **Run the API:**

	- `cd TechnicalExercice`
	- `dotnet run`

	The API uses an in-memory EF provider by default for convenience during development. Open `Program.cs` to change the DB provider or connection string.

**Tests**
- **Run all tests:**

	- `dotnet test ./TechnicalExercice.Tests/TechnicalExercice.Tests.csproj`

- The tests use `WebApplicationFactory<Program>` for integration tests. To avoid host/test binary mismatches, the test project references `Microsoft.AspNetCore.Mvc.Testing` aligned with the app framework version.

**Key Endpoints**
- `GET /client` : returns all clients (via `GetClients`).
- `POST /client/create` : create client (accepts `CreateRequest` JSON body).
- `POST /client/add-product` : add product to a client's basket. `AddProductRequest` supports `BasketId` as optional (`Guid?`) — when omitted the API creates a new basket.
- `GET /client/{clientId}/basket/{basketId}/total-to-pay` : returns total for the basket using `CalculateTotalBasket` use-case.

**Important Implementation Notes & Decisions**
- **Endpoint registration & DI**: Endpoints are discovered by `EndpointExtensions`. Instances are created without resolving request-scoped services at startup; handlers accept scoped services (like `CalculateTotalBasket`) as parameters so DI resolves them per-request.
- **Scoped services**: Services that depend on scoped resources (DbContext) are registered as scoped (`AddScoped<>`) and must be resolved per-request — handlers use parameter injection to avoid resolving scoped services from the root provider.


**Testing & Debugging Tips**
- When an integration test returns HTTP 500, inspect the response body before calling `EnsureSuccessStatusCode()` and run the host in `Development` to get the DeveloperExceptionPage in the test host (use `WithWebHostBuilder(builder => builder.UseEnvironment("Development"))`).
- To see the request body or server exception during tests, temporarily add a logging middleware early in the pipeline (only for debugging) to print request bodies and exceptions.


**Useful commands**
- Restore & build: `dotnet restore && dotnet build`
- Run API: `dotnet run --project TechnicalExercice`
- Run tests: `dotnet test ./TechnicalExercice.Tests/TechnicalExercice.Tests.csproj`

