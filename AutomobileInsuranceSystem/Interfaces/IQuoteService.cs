using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;

namespace AutomobileInsuranceSystem.Interfaces
{
    public interface IQuoteService
    {
        Task<Quote> CreateQuoteAsync(QuoteDTO dto);
        Task<List<Quote>> GetQuotesByProposalAsync(int proposalId);
    }
}
