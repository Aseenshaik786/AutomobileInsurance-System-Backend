using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Repositories
{
    public class PolicyRepository : IRepository<int, Policy>
    {
        private readonly AppDbContext _context;
        public PolicyRepository(AppDbContext context) => _context = context;

        public async Task<Policy> Add(Policy entity) { _context.Policies.Add(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task<Policy> Update(int key, Policy entity) { var item = await _context.Policies.FindAsync(key); if (item == null) return null; _context.Entry(item).CurrentValues.SetValues(entity); await _context.SaveChangesAsync(); return item; }
        public async Task<Policy> Delete(int key) { var item = await _context.Policies.FindAsync(key); if (item == null) return null; _context.Policies.Remove(item); await _context.SaveChangesAsync(); return item; }
        public async Task<Policy> GetById(int key) => await _context.Policies.FindAsync(key);
        public async Task<IEnumerable<Policy>> GetAll() => await _context.Policies.ToListAsync();
    }
}
