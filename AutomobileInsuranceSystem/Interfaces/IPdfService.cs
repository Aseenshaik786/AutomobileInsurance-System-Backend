// Interfaces/IPdfService.cs
using AutomobileInsuranceSystem.Models;

namespace AutomobileInsuranceSystem.Interfaces
{
    public interface IPdfService
    {
        byte[] GeneratePolicyPdf(Proposal proposal, Quote quote, User user);
    }
}
