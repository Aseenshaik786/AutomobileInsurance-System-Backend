// Controllers/PaymentsController.cs
using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
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
    public class PaymentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "User")]  // User role can make payments
        public async Task<IActionResult> MakePayment([FromBody] PaymentDTO dto)
        {
            // Find quote
            var quote = await _context.Quotes
                .Include(q => q.Proposal)
                .ThenInclude(p => p.Policy)
                .FirstOrDefaultAsync(q => q.QuoteId == dto.QuoteId);

            if (quote == null)
                return NotFound($"Quote with ID {dto.QuoteId} not found.");

            // Optional: Check if current user owns the proposal
            var userIdClaim = User.FindFirst("nameid")?.Value;
            if (userIdClaim == null || quote.Proposal.UserId != int.Parse(userIdClaim))
                return Forbid("You are not authorized to make payment for this quote.");

            // Validate payment amount >= quote total premium (optional)
            if (dto.PaymentAmount < quote.TotalPremium)
                return BadRequest("Payment amount cannot be less than the total premium.");

            var payment = new Payment
            {
                QuoteId = dto.QuoteId,
                PaymentAmount = dto.PaymentAmount,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = dto.PaymentStatus
            };

            _context.Payments.Add(payment);

            // Optionally update proposal status after payment
            quote.Proposal.PolicyStatus = "Payment Completed";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Payment successful and proposal status updated.",
                Payment = payment,
                UpdatedProposalStatus = quote.Proposal.PolicyStatus
            });
        }
    }

}
