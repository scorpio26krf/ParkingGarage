# ParkingGarage

A multi‑project .NET 8 solution demonstrating domain‑driven design, TDD, and Minimal API implementation for a parking garage system. This project is being rebuilt from the ground up as a professional portfolio example showcasing clean architecture and modern .NET development practices.

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
            (Minimal API implementation coming next)

        ParkingGarage.Tests/
            CarTests.cs
            GarageTests.cs
            ParkingReceiptTests.cs

---

## Domain Model

The core domain contains three foundational classes:

- Car: represents a vehicle entering and exiting the garage  
- Garage: manages capacity, entry rules, exit rules, and pricing  
- ParkingReceipt: returned when a car exits, containing timestamps, duration, and total price  

These classes are intentionally minimal, focused, and fully validated through test‑driven development.

---

## Domain & Testing Status

The domain layer is fully implemented and covered by a complete NUnit test suite.  
All core behaviors—including car entry, exit, receipt generation, pricing, and garage capacity rules—are validated through deterministic unit tests.

### Completed Test Coverage
- CarTests.cs: validates car lifecycle (enter, exit, timestamps)  
- GarageTests.cs: validates capacity rules, duplicate plates, exit behavior, pricing, and error handling  
- ParkingReceiptTests.cs: validates receipt construction, duration, and pricing fields  

This establishes a stable, well‑tested foundation for the upcoming Minimal API layer.

---

## Goals

- Clean domain architecture  
- Deterministic unit tests  
- Minimal API exposing garage operations  
- Professional folder and solution structure  
- Clear separation of concerns  

---

## Status

The domain model and full test suite are complete.  
API implementation will follow next.