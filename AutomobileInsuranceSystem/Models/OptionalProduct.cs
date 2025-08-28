namespace AutomobileInsuranceSystem.Models
{
    public class OptionalProduct
    {
        public int OptionalProductId { get; set; }
        public string Name { get; set; }

        public ICollection<ProposalOptionalProduct> ProposalOptionalProducts { get; set; }
    }
}
