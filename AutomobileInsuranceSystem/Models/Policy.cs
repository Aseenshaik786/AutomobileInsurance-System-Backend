namespace AutomobileInsuranceSystem.Models
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePremium { get; set; }
        public int DurationMonths { get; set; }
        public string AddOns { get; set; }

        public ICollection<Proposal> Proposals { get; set; }
    }

}
