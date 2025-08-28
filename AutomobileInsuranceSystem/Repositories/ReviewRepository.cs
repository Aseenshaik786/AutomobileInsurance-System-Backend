using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Repositories
{
    public class ReviewRepository : IRepository<int, Review>
    {
        private readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context) => _context = context;

        public async Task<Review> Add(Review entity) { _context.Reviews.Add(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task<Review> Update(int key, Review entity) { var item = await _context.Reviews.FindAsync(key); if (item == null) return null; _context.Entry(item).CurrentValues.SetValues(entity); await _context.SaveChangesAsync(); return item; }
        public async Task<Review> Delete(int key) { var item = await _context.Reviews.FindAsync(key); if (item == null) return null; _context.Reviews.Remove(item); await _context.SaveChangesAsync(); return item; }
        public async Task<Review> GetById(int key) => await _context.Reviews.FindAsync(key);
        public async Task<IEnumerable<Review>> GetAll() => await _context.Reviews.ToListAsync();
    }
}
