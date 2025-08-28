namespace AutomobileInsuranceSystem.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public string VehicleType { get; set; }
        public string Make { get; set; }
        public decimal EnginePerformanceKW { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public int NumberOfSeats { get; set; }
        public string FuelType { get; set; }
        public decimal ListPrice { get; set; }
        public string LicensePlateNumber { get; set; }
        public int AnnualMileage { get; set; }

        public User? User { get; set; }  // nullable
        public ICollection<Proposal>? Proposals { get; set; } // nullable
    }
}
