namespace AutomobileInsuranceSystem.Models.DTOs
{
    public class PaymentDTO
    {
        public int QuoteId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentStatus { get; set; } = "Pending"; // or "Completed" etc.
    }


}
