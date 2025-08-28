using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Officer,Admin")] // ✅ Restrict quote creation
    public class QuotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuotesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "User,Officer,Admin")]
        public async Task<IActionResult> GetQuotesByUser(int userId)
        {
            var loggedInUserIdClaim = User.FindFirst("nameid")?.Value;
            if (loggedInUserIdClaim == null)
                return Unauthorized("User ID claim missing.");

            var loggedInUserId = int.Parse(loggedInUserIdClaim);
            var userRoles = User.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

            bool isAdminOrOfficer = userRoles.Contains("Admin") || userRoles.Contains("Officer");

            // Users can only get their own quotes
            if (!isAdminOrOfficer && userId != loggedInUserId)
            {
                return Forbid("You are not authorized to access quotes for other users.");
            }

            // Fetch quotes linked to proposals of this user
            var quotes = await _context.Quotes
                .Include(q => q.Proposal)
                .ThenInclude(p => p.Policy)
                .Where(q => q.Proposal.UserId == userId)
                .Select(q => new
                {
                    q.QuoteId,
                    q.ProposalId,
                    q.TotalPremium,
                    q.SentToEmail,
                    q.QuoteDate,
                    Proposal = new
                    {
                        q.Proposal.PolicyStatus,
                        PolicyName = q.Proposal.Policy.Name,
                        q.Proposal.StartDate
                    }
                })
                .ToListAsync();

            if (quotes == null || quotes.Count == 0)
                return NotFound("No quotes found for this user.");

            return Ok(quotes);
        }


        [HttpPost]
        public async Task<IActionResult> CreateQuote(QuoteDTO dto)
        {
            // ✅ Check if proposal exists
            var proposal = await _context.Proposals.FindAsync(dto.ProposalId);
            if (proposal == null)
                return NotFound($"Proposal with ID {dto.ProposalId} not found.");

            // ✅ Prevent duplicate quotes for the same proposal
            var existingQuote = await _context.Quotes
                .FirstOrDefaultAsync(q => q.ProposalId == dto.ProposalId);
            if (existingQuote != null)
                return BadRequest("Quote already exists for this proposal.");

            // ✅ Create new quote
            var quote = new Quote
            {
                ProposalId = dto.ProposalId,
                TotalPremium = dto.TotalPremium,
                SentToEmail = dto.SentToEmail
            };

            _context.Quotes.Add(quote);

            // ✅ Update proposal status
            proposal.PolicyStatus = "Quote Generated";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Quote created successfully and proposal status updated to 'Quote Generated'.",
                Quote = quote
            });
        }
    }
}
