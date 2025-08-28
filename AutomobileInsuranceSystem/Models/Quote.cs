namespace AutomobileInsuranceSystem.Models
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public int ProposalId { get; set; }
        public decimal TotalPremium { get; set; }
        public DateTime QuoteDate { get; set; } = DateTime.UtcNow;
        public string SentToEmail { get; set; }

        public Proposal Proposal { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }

}
