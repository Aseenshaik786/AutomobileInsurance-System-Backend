namespace AutomobileInsuranceSystem.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int QuoteId { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string PaymentStatus { get; set; } = "Pending"; // Pending or Completed

        public Quote Quote { get; set; }
    }

}
