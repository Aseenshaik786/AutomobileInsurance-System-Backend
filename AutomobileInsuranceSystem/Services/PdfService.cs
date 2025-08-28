// Services/PdfService.cs
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using System.Text;

namespace AutomobileInsuranceSystem.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GeneratePolicyPdf(Proposal proposal, Quote quote, User user)
        {
            // For simplicity: return a byte[] text placeholder.
            var sb = new StringBuilder();
            sb.AppendLine("Policy Document");
            sb.AppendLine($"Policy Holder: {user.FirstName} {user.LastName} ({user.Email})");
            sb.AppendLine($"Proposal Id: {proposal.ProposalId}");
            sb.AppendLine($"Quote Id: {quote.QuoteId}, Premium: {quote.TotalPremium}");
            sb.AppendLine($"Status: {proposal.PolicyStatus}");
            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
