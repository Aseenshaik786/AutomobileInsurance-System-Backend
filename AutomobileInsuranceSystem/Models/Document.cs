namespace AutomobileInsuranceSystem.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int ProposalId { get; set; }
        public string DocumentType { get; set; }
        public string FileUrl { get; set; }
        public string UploadedBy { get; set; } // User or Officer
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

        public Proposal Proposal { get; set; }
    }

}
