using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;

namespace AutomobileInsuranceSystem.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> MakePaymentAsync(PaymentDTO dto);
        Task<List<Payment>> GetPaymentsByQuoteAsync(int quoteId);
    }
}
