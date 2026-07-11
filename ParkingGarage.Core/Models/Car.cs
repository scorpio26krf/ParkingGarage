namespace ParkingGarage.Core.Models
{
    public class Car
    {
        public string LicensePlate { get; set; }
        public DateTime EnteredAt { get; set; }
        public DateTime? ExitedAt { get; private set; }

        public Car(string licensePlate, DateTime enteredAt)
        {
            LicensePlate = licensePlate;
            EnteredAt = enteredAt;
        }

        public void Exit(DateTime exitTime)
        {
            ExitedAt = exitTime;
        }
    }
}
