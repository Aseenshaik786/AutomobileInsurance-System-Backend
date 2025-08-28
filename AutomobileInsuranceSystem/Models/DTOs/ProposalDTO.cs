namespace AutomobileInsuranceSystem.Models.DTOs
{
    public class ProposalDTO
    {
        public int VehicleId { get; set; }
        public int ProposalId { get; set; }
        public int PolicyId { get; set; }
        public int UserId { get; set; }
        public int? OfficerId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal InsuranceSum { get; set; }
        public string MeritRating { get; set; }
        public string DamageInsurance { get; set; }
        public string CourtesyCar { get; set; }
        public string PolicyStatus { get; set; }
        public List<int> OptionalProductIds { get; set; }
    }

}
