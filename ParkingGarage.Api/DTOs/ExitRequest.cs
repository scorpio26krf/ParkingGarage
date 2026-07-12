using System.Text.Json.Serialization;

namespace ParkingGarage.Api.DTOs;

public record ExitRequest([property: JsonPropertyName("licensePlate")] string LicensePlate);