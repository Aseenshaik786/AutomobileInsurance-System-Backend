using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Repositories
{
    public class PaymentRepository : IRepository<int, Payment>
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context) => _context = context;

        public async Task<Payment> Add(Payment entity) { _context.Payments.Add(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task<Payment> Update(int key, Payment entity) { var item = await _context.Payments.FindAsync(key); if (item == null) return null; _context.Entry(item).CurrentValues.SetValues(entity); await _context.SaveChangesAsync(); return item; }
        public async Task<Payment> Delete(int key) { var item = await _context.Payments.FindAsync(key); if (item == null) return null; _context.Payments.Remove(item); await _context.SaveChangesAsync(); return item; }
        public async Task<Payment> GetById(int key) => await _context.Payments.FindAsync(key);
        public async Task<IEnumerable<Payment>> GetAll() => await _context.Payments.ToListAsync();
    }
}
