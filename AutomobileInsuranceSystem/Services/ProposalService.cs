using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Services
{
    public class ProposalService : IProposalService
    {
        private readonly AppDbContext _context;

        public ProposalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Proposal> CreateProposalAsync(int userId, ProposalDTO dto)
        {
            var proposal = new Proposal
            {
                UserId = userId,
                VehicleId = dto.VehicleId,
                PolicyId = dto.PolicyId,
                StartDate = dto.StartDate,
                InsuranceSum = dto.InsuranceSum,
                MeritRating = dto.MeritRating,
                DamageInsurance = dto.DamageInsurance,
                CourtesyCar = dto.CourtesyCar,
                PolicyStatus = "Proposal Submitted",
                CreatedAt = DateTime.UtcNow
            };

            _context.Proposals.Add(proposal);
            await _context.SaveChangesAsync();

            if (dto.OptionalProductIds != null)
            {
                foreach (var optId in dto.OptionalProductIds)
                {
                    _context.ProposalOptionalProducts.Add(new ProposalOptionalProduct
                    {
                        ProposalId = proposal.ProposalId,
                        OptionalProductId = optId
                    });
                }
                await _context.SaveChangesAsync();
            }

            return proposal;
        }

        public async Task<List<Proposal>> GetProposalsByUserAsync(int userId)
        {
            return await _context.Proposals
                .Where(p => p.UserId == userId)
                .Include(p => p.Policy)
                .Include(p => p.Vehicle)
                .ToListAsync();
        }
    }
}
