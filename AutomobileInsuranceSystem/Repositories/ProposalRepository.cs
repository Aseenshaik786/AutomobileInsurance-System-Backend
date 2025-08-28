using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Repositories
{
    public class ProposalRepository : IRepository<int, Proposal>
    {
        private readonly AppDbContext _context;
        public ProposalRepository(AppDbContext context) => _context = context;

        public async Task<Proposal> Add(Proposal entity) { _context.Proposals.Add(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task<Proposal> Update(int key, Proposal entity) { var item = await _context.Proposals.FindAsync(key); if (item == null) return null; _context.Entry(item).CurrentValues.SetValues(entity); await _context.SaveChangesAsync(); return item; }
        public async Task<Proposal> Delete(int key) { var item = await _context.Proposals.FindAsync(key); if (item == null) return null; _context.Proposals.Remove(item); await _context.SaveChangesAsync(); return item; }
        public async Task<Proposal> GetById(int key) => await _context.Proposals.FindAsync(key);
        public async Task<IEnumerable<Proposal>> GetAll() => await _context.Proposals.ToListAsync();
    }

}
