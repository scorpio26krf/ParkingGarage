namespace ParkingGarage.Core.Models
{
    public class ParkingReceipt
    {
        public string LicensePlate { get; set; }
        public DateTime EnteredAt { get; set; }
        public DateTime ExitedAt { get; set; }
        public int DurationMinutes { get; set; }
        public decimal TotalPrice { get; set; }

        public ParkingReceipt(string licensePlate, DateTime enteredAt, DateTime exitedAt, int durationMinutes, decimal totalPrice)
        {
            LicensePlate = licensePlate;
            EnteredAt = enteredAt;
            ExitedAt = exitedAt;
            DurationMinutes = durationMinutes;
            TotalPrice = totalPrice;
        }
    }
}
