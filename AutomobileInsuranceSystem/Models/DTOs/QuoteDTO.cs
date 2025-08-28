namespace AutomobileInsuranceSystem.Models.DTOs
{
    public class QuoteDTO
    {
        public int ProposalId { get; set; }
        public decimal TotalPremium { get; set; }
        public string? SentToEmail { get; set; }
    }
}
