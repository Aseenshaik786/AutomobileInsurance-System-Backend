using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutomobileInsuranceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "User")]  // Only authenticated users can upload documents here
        public async Task<IActionResult> UploadDocumentForProposal([FromBody] Models.DTOs.DocumentDTO dto)
        {
            var proposal = await _context.Proposals.FindAsync(dto.ProposalId);
            if (proposal == null)
                return NotFound($"Proposal with ID {dto.ProposalId} not found.");

            // Optionally check if the current user owns the proposal before allowing upload
            var userIdClaim = User.FindFirst("nameid")?.Value;
            if (userIdClaim == null || proposal.UserId != int.Parse(userIdClaim))
                return Forbid("You do not have permission to upload documents for this proposal.");

            var document = new Document
            {
                ProposalId = dto.ProposalId,
                DocumentType = dto.DocumentType,
                FileUrl = dto.FileUrl,
                UploadedBy = dto.UploadedBy,
                UploadedDate = DateTime.UtcNow
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Document uploaded successfully.", Document = document });
        }

    }
}
