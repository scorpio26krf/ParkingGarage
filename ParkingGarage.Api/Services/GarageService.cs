using ParkingGarage.Core.Models;

namespace ParkingGarage.Api.Services;

// Thin service layer between API and domain.
public class GarageService
{
    private readonly Garage _garage;

    public GarageService(Garage garage)
    {
        _garage = garage;
    }

    // Attempts to park a car
    public bool EnterCar(string licensePlate, DateTime now)
        => _garage.CarEnters(licensePlate, now);

    // Attempts to exit a car
    public ParkingReceipt ExitCar(string licensePlate, DateTime now)
        => _garage.CarExits(licensePlate, now);

    public IReadOnlyList<Car> GetParkedCars()
        => _garage.ParkedCars;

    public int GetAvailableSpaces()
        => _garage.Capacity - _garage.ParkedCars.Count;

    public int Capacity => _garage.Capacity;
}

