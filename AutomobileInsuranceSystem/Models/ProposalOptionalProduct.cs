namespace AutomobileInsuranceSystem.Models
{
    public class ProposalOptionalProduct
    {
        public int ProposalId { get; set; }  
        public Proposal Proposal { get; set; }

        public int OptionalProductId { get; set; }

     
        public OptionalProduct OptionalProduct { get; set; }
    }

}
