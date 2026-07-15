# ParkingGarage

A clean, modern .NET 8 multi‑project solution that models a parking garage and exposes its behavior through a minimal API. The project emphasizes domain‑driven design, clear separation of concerns, EF Core persistence, and full test coverage. It serves as a polished example of how I structure small backend services: readable domain logic, focused APIs, deterministic tests, and a professional project layout.

---

## Project Structure

    ParkingGarage/
        ParkingGarage.sln
        README.md

        ParkingGarage.Core/
            Models/
                Car.cs
                Garage.cs
                PricingRule.cs
                Receipt.cs

        ParkingGarage.Api/
            DTOs/
                EnterRequest.cs
                ExitRequest.cs
                CreatePricingRuleRequest.cs
                ReceiptResponse.cs
            Endpoints/
                GarageEndpoints.cs
            Services/
                GarageService.cs
            Middleware/
                ErrorHandlingMiddleware.cs
            Repositories/
                PricingRuleRepository.cs
                ReceiptRepository.cs
            Data/
                GarageDbContext.cs
            Program.cs

        ParkingGarage.Tests.Core/
            CarTests.cs
            GarageTests.cs
            ReceiptTests.cs

        ParkingGarage.Tests.Api/
            CustomWebApplicationFactory.cs
            GarageApiTests.cs

---

## Domain Model

The domain layer contains the core business rules of the parking garage. It is intentionally small, readable, and fully validated.

- Car: represents a vehicle with entry and exit timestamps  
- Garage: manages capacity, duplicate license plates, entry/exit rules, pricing rule application, and receipt creation  
- PricingRule: defines how parking is charged (hourly rate, grace period, optional max daily charge)  
- Receipt: returned when a car exits, containing entry time, exit time, total hours, total amount, and the applied pricing rule  

---

## API Layer

The API exposes the main garage operations through minimal API endpoints.

### Garage Operations
- POST /enter: park a car  
- POST /exit: exit a car and receive a receipt  
- GET /cars: list currently parked cars  
- GET /spaces: available spaces  
- GET /status: garage summary  

### Pricing Rules
- POST /pricing-rules: create a pricing rule  
- GET /pricing-rules: list pricing rules  
- POST /pricing-rules/{id}/activate: set active pricing rule  

### Validation
- Ensures required fields are present  
- Ensures values fall within expected ranges  
- Returns consistent error messages  

### Error Handling
- Global middleware for structured JSON error responses  
- Logs unhandled exceptions  
- Provides consistent behavior across endpoints  

---

## Persistence Layer

The project uses EF Core + SQLite to persist pricing rules and receipts.

Migrations can be applied using:

    dotnet ef migrations add InitialCreate --project .\ParkingGarage.Api\
    dotnet ef database update --project .\ParkingGarage.Api\

Repositories provide a clean abstraction between EF Core and the service layer.

---

## Testing

The solution includes full test coverage across both layers.

### Domain Tests
- Validate car lifecycle  
- Validate garage capacity rules  
- Validate pricing rule behavior  
- Validate receipt generation  

### API Tests
- Validate endpoint behavior  
- Validate request validation  
- Validate error handling  
- Validate pricing rule activation  
- Validate receipt creation  

Tests use WebApplicationFactory to run the API in-memory, ensuring deterministic results.

---

## Why This Project

This project demonstrates how I approach backend development:

- Clean separation between layers  
- Minimal API usage  
- Domain‑driven design principles  
- Practical EF Core persistence  
- Deterministic unit and integration tests  
- Readable, maintainable code  
- A realistic example of a small but complete .NET service  

---

## Future Improvements

- Add GET /receipts endpoint  
- Add GET /pricing-rules/active endpoint  
- Add Swagger/OpenAPI documentation  
- Add logging inside GarageService  
- Add UI (React/Angular/Blazor)  
- Add Docker support  
- Add CI/CD pipeline  
- Add historical garage activity tracking  

---

## Version History

- Phase 1–4: Domain + API + Tests  
- Phase 5: EF Core persistence + migrations  
- Phase 6: Validation + error handling + README polish  