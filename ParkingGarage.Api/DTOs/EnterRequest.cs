using System.Text.Json.Serialization;

namespace ParkingGarage.Api.DTOs;

public record EnterRequest([property: JsonPropertyName("licensePlate")] string LicensePlate);

