using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;

namespace AutomobileInsuranceSystem.Interfaces
{
    public interface IProposalService
    {
        Task<Proposal> CreateProposalAsync(int userId, ProposalDTO dto);
        Task<List<Proposal>> GetProposalsByUserAsync(int userId);
    }
}
