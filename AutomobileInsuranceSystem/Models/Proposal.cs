namespace AutomobileInsuranceSystem.Models
{
    public class Proposal
    {
        public int ProposalId { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public int PolicyId { get; set; }
        public int? OfficerId { get; set; }

        public DateTime StartDate { get; set; }
        public decimal InsuranceSum { get; set; }
        public string MeritRating { get; set; }
        public string DamageInsurance { get; set; }
        public string CourtesyCar { get; set; }
        public string PolicyStatus { get; set; } = "Proposal Submitted";

        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public User Officer { get; set; }
        public Vehicle Vehicle { get; set; }
        public Policy Policy { get; set; }

        public ICollection<Quote> Quotes { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<EmailReminder> EmailReminders { get; set; }
        public ICollection<ProposalOptionalProduct> ProposalOptionalProducts { get; set; }
    }
}
