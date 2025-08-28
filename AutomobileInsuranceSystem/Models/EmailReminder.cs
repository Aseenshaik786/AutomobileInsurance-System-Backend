namespace AutomobileInsuranceSystem.Models
{
    public class EmailReminder
    {
        public int ReminderId { get; set; }
        public int ProposalId { get; set; }
        public DateTime EmailSentDate { get; set; }

        public Proposal Proposal { get; set; }
    }

}
