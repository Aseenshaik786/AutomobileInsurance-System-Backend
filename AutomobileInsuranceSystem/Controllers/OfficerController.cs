// Controllers/OfficerController.cs
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
    [Authorize(Roles = "Officer,Admin")]
    public class OfficerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OfficerController(AppDbContext context) { _context = context; }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var list = await _context.Proposals.Where(p => p.PolicyStatus == "Proposal Submitted").ToListAsync();
            return Ok(list);
        }

        [HttpPut("{id}/request-docs")]
        public async Task<IActionResult> RequestDocuments(int id, [FromBody] string message)
        {
            var p = await _context.Proposals.FindAsync(id);
            if (p == null) return NotFound();
            p.PolicyStatus = "Documents Requested";
            // Optionally write a record in a Requests table or send email
            await _context.SaveChangesAsync();
            return Ok(new { message = "Documents requested", proposalId = id });
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveProposal(int id, [FromBody] QuoteDTO dto)
        {
            var p = await _context.Proposals.FindAsync(id);
            if (p == null) return NotFound();

            if (await _context.Quotes.AnyAsync(q => q.ProposalId == id))
                return BadRequest("Quote already exists for this proposal.");

            var quote = new Quote
            {
                ProposalId = id,
                TotalPremium = dto.TotalPremium,
                QuoteDate = DateTime.UtcNow,
                SentToEmail = dto.SentToEmail
            };
            _context.Quotes.Add(quote);
            p.PolicyStatus = "Quote Generated";
            await _context.SaveChangesAsync();
            // Optionally send email with quote here
            return Ok(new { message = "Approved and quote generated", quote });
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectProposal(int id, [FromBody] string reason)
        {
            var p = await _context.Proposals.FindAsync(id);
            if (p == null) return NotFound();
            p.PolicyStatus = "Rejected";
            await _context.SaveChangesAsync();
            return Ok(new { message = "Proposal rejected", reason });
        }
    }
}
