using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Repositories
{
    public class QuoteRepository : IRepository<int, Quote>
    {
        private readonly AppDbContext _context;
        public QuoteRepository(AppDbContext context) => _context = context;

        public async Task<Quote> Add(Quote entity) { _context.Quotes.Add(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task<Quote> Update(int key, Quote entity) { var item = await _context.Quotes.FindAsync(key); if (item == null) return null; _context.Entry(item).CurrentValues.SetValues(entity); await _context.SaveChangesAsync(); return item; }
        public async Task<Quote> Delete(int key) { var item = await _context.Quotes.FindAsync(key); if (item == null) return null; _context.Quotes.Remove(item); await _context.SaveChangesAsync(); return item; }
        public async Task<Quote> GetById(int key) => await _context.Quotes.FindAsync(key);
        public async Task<IEnumerable<Quote>> GetAll() => await _context.Quotes.ToListAsync();
    }

}
