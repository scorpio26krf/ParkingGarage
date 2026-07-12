# ParkingGarage

A .NET 8 multi‑project solution that models a parking garage and exposes its behavior through a minimal API. The project focuses on clear separation between the domain, API, and tests, with full coverage across both layers.

---

## Project Structure

    ParkingGarage/
        ParkingGarage.sln
        README.md

        ParkingGarage.Core/
            Models/
                Car.cs
                Garage.cs
                ParkingReceipt.cs

        ParkingGarage.Api/
            DTOs/
                EnterRequest.cs
                ExitRequest.cs
                ReceiptResponse.cs
            Endpoints/
                GarageEndpoints.cs
            Services/
                GarageService.cs
            Program.cs

        ParkingGarage.Tests.Core/
            CarTests.cs
            GarageTests.cs
            ParkingReceiptTests.cs

        ParkingGarage.Tests.Api/
            CustomWebApplicationFactory.cs
            GarageApiTests.cs

---

## Domain Model

The core domain contains three main classes:

- Car: represents a vehicle with entry and exit timestamps  
- Garage: manages capacity, duplicate plates, entry/exit rules, and receipt creation  
- ParkingReceipt: returned when a car exits, containing duration and pricing  

The domain is intentionally simple and easy to follow.

---

## API Layer

The API exposes the main garage operations:

- POST /enter: park a car  
- POST /exit: exit a car and get a receipt  
- GET /cars: list parked cars  
- GET /spaces: available spaces  
- GET /status: garage summary  

DTOs are small and focused. A thin GarageService sits between the API and domain.

---

## Testing

The solution includes full test coverage:

- Domain tests: validate car behavior, garage rules, and receipt generation  
- API tests: validate all endpoints end‑to‑end using WebApplicationFactory  
- CustomWebApplicationFactory: ensures each test runs with a fresh garage  

All tests pass.

---

## Why This Project

This project demonstrates:

- Clear separation between layers  
- Minimal API usage  
- Practical test setup  
- Readable, maintainable code  
- A realistic example of how I structure small .NET projects  