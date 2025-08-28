// Models/DTOs/VehicleDTO.cs
namespace AutomobileInsuranceSystem.Models.DTOs
{
    public class VehicleDTO
    {
        public string VehicleType { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public decimal EnginePerformanceKW { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public int NumberOfSeats { get; set; }
        public string FuelType { get; set; } = string.Empty;
        public decimal ListPrice { get; set; }
        public string LicensePlateNumber { get; set; } = string.Empty;
        public int AnnualMileage { get; set; }
    }
}
