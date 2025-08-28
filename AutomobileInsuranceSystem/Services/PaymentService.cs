using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> MakePaymentAsync(PaymentDTO dto)
        {
            var payment = new Payment
            {
                QuoteId = dto.QuoteId,
                PaymentAmount = dto.PaymentAmount,
                PaymentStatus = dto.PaymentStatus,
                PaymentDate = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<List<Payment>> GetPaymentsByQuoteAsync(int quoteId)
        {
            return await _context.Payments.Where(p => p.QuoteId == quoteId).ToListAsync();
        }
    }
}
