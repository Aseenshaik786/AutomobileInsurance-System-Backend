using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Repositories
{
    public class UserRepository : IRepository<int, User>
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) => _context = context;

        public async Task<User> Add(User entity) { _context.Users.Add(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task<User> Update(int key, User entity) { var item = await _context.Users.FindAsync(key); if (item == null) return null; _context.Entry(item).CurrentValues.SetValues(entity); await _context.SaveChangesAsync(); return item; }
        public async Task<User> Delete(int key) { var item = await _context.Users.FindAsync(key); if (item == null) return null; _context.Users.Remove(item); await _context.SaveChangesAsync(); return item; }
        public async Task<User> GetById(int key) => await _context.Users.FindAsync(key);
        public async Task<IEnumerable<User>> GetAll() => await _context.Users.ToListAsync();
    }
}
