# ParkingGarage

A multi‑project .NET 8 solution demonstrating domain‑driven design, TDD, and Minimal API implementation for a parking garage system.

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
        (NUnit test suite coming next)

---

## Domain Model (Current)

The core domain contains three foundational classes:

- **Car** � represents a vehicle entering and exiting the garage  
- **Garage** � manages capacity, entry rules, exit rules, and pricing  
- **ParkingReceipt** � returned when a car exits, containing timestamps, duration, and total price  

These classes are intentionally minimal and will be expanded through test-driven development.

---

## Goals

- Clean domain architecture  
- Deterministic unit tests  
- Minimal API exposing garage operations  
- Professional folder and solution structure  
- Clear separation of concerns  

---

## Status

The domain model and solution structure are complete.  
API and test implementation will follow next.