// Controllers/ProposalsController.cs
using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AutomobileInsuranceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProposalsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProposalsController(AppDbContext context) { _context = context; }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> CreateProposal([FromBody] ProposalDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var proposal = new Proposal
            {
                VehicleId = dto.VehicleId,
                PolicyId = dto.PolicyId,
                UserId = dto.UserId,
                OfficerId = dto.OfficerId,
                StartDate = dto.StartDate,
                InsuranceSum = dto.InsuranceSum,
                MeritRating = dto.MeritRating,
                DamageInsurance = dto.DamageInsurance,
                CourtesyCar = dto.CourtesyCar,
                PolicyStatus = dto.PolicyStatus
            };

            _context.Proposals.Add(proposal);
            await _context.SaveChangesAsync();

            // Assuming ProposalOptionalProduct is a join entity between Proposal and OptionalProduct
            if (dto.OptionalProductIds != null && dto.OptionalProductIds.Any())
            {
                foreach (var optProdId in dto.OptionalProductIds)
                {
                    _context.ProposalOptionalProducts.Add(new ProposalOptionalProduct
                    {
                        ProposalId = proposal.ProposalId,
                        OptionalProductId = optProdId
                    });
                }
                await _context.SaveChangesAsync();
            }

            var fullProposal = await _context.Proposals
                .Include(p => p.User)
                .Include(p => p.Vehicle)
                .Include(p => p.Policy)
                .Include(p => p.Officer)
                .Include(p => p.ProposalOptionalProducts)
                    .ThenInclude(pop => pop.OptionalProduct)
                .FirstOrDefaultAsync(p => p.ProposalId == proposal.ProposalId);

            if (fullProposal == null)
                return NotFound();

            return CreatedAtAction(nameof(GetProposalById), new { id = fullProposal.ProposalId }, fullProposal);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProposalById(int id)
        {
            var proposal = await _context.Proposals
                .Include(p => p.User)
                .Include(p => p.Vehicle)
                .Include(p => p.Policy)
                .Include(p => p.Officer)
                .FirstOrDefaultAsync(p => p.ProposalId == id);

            if (proposal == null)
                return NotFound();

            return Ok(proposal);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyProposals()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID not found in token.");
            int userId = int.Parse(userIdClaim);

            var proposals = await _context.Proposals
                .Where(p => p.UserId == userId)
                .Include(p => p.Policy)
                .Include(p => p.Vehicle)
                .Include(p => p.ProposalOptionalProducts)
                    .ThenInclude(pop => pop.OptionalProduct)
                .Select(p => new ProposalDTO
                {
                    ProposalId = p.ProposalId,
                    VehicleId = p.VehicleId,
                    PolicyId = p.PolicyId,
                    StartDate = p.StartDate,
                    InsuranceSum = p.InsuranceSum,
                    MeritRating = p.MeritRating,
                    DamageInsurance = p.DamageInsurance,
                    CourtesyCar = p.CourtesyCar,
                    PolicyStatus = p.PolicyStatus,
                    OptionalProductIds = p.ProposalOptionalProducts
                        .Select(op => op.OptionalProductId)
                        .ToList()
                })
                .ToListAsync();

            return Ok(proposals);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposal(int id)
        {
            var p = await _context.Proposals.FindAsync(id);
            if (p == null) return NotFound();
            _context.Proposals.Remove(p);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchProposals(string? keyword, int page = 1, int pageSize = 10)
        {
            var q = _context.Proposals
                .Include(p => p.Policy)
                .Include(p => p.Vehicle)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                // Check if keyword is numeric
                if (int.TryParse(keyword, out int id))
                {
                    // First check if a proposal exists with this ProposalId
                    var proposal = await q.FirstOrDefaultAsync(p => p.ProposalId == id);
                    if (proposal != null)
                    {
                        // Return just that proposal
                        return Ok(new { TotalCount = 1, Page = 1, PageSize = 1, Items = new List<object> { proposal } });
                    }

                    // Otherwise, treat it as UserId
                    q = q.Where(p => p.UserId == id);
                }
                else
                {
                    // String-based search (MeritRating or CourtesyCar)
                    keyword = keyword.ToLower();
                    q = q.Where(p =>
                        (p.MeritRating != null && p.MeritRating.ToLower().Contains(keyword)) ||
                        (p.CourtesyCar != null && p.CourtesyCar.ToLower().Contains(keyword)));
                }
            }

            var total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { TotalCount = total, Page = page, PageSize = pageSize, Items = items });
        }

    }
}
