using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly AppDbContext _context;

        public QuoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Quote> CreateQuoteAsync(QuoteDTO dto)
        {
            var quote = new Quote
            {
                ProposalId = dto.ProposalId,
                TotalPremium = dto.TotalPremium,
                SentToEmail = dto.SentToEmail,
                QuoteDate = DateTime.UtcNow
            };

            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();
            return quote;
        }

        public async Task<List<Quote>> GetQuotesByProposalAsync(int proposalId)
        {
            return await _context.Quotes.Where(q => q.ProposalId == proposalId).ToListAsync();
        }
    }

}
