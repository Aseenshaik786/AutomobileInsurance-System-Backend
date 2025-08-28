namespace AutomobileInsuranceSystem.Models.DTOs
{
    public class DocumentDTO
    {
        public int ProposalId { get; set; }
        public string DocumentType { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string UploadedBy { get; set; } = string.Empty;
    }
}
