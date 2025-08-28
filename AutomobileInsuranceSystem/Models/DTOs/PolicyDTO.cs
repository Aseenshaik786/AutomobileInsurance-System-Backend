// Models/DTOs/PolicyDTO.cs
namespace AutomobileInsuranceSystem.Models.DTOs
{
    public class PolicyDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal BasePremium { get; set; }
        public int DurationMonths { get; set; }
        public string? AddOns { get; set; }
    }
}
