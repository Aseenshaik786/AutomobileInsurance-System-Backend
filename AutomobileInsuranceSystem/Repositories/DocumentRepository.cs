using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Repositories
{
    public class DocumentRepository : IRepository<int, Document>
    {
        private readonly AppDbContext _context;
        public DocumentRepository(AppDbContext context) => _context = context;

        public async Task<Document> Add(Document entity) { _context.Documents.Add(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task<Document> Update(int key, Document entity) { var item = await _context.Documents.FindAsync(key); if (item == null) return null; _context.Entry(item).CurrentValues.SetValues(entity); await _context.SaveChangesAsync(); return item; }
        public async Task<Document> Delete(int key) { var item = await _context.Documents.FindAsync(key); if (item == null) return null; _context.Documents.Remove(item); await _context.SaveChangesAsync(); return item; }
        public async Task<Document> GetById(int key) => await _context.Documents.FindAsync(key);
        public async Task<IEnumerable<Document>> GetAll() => await _context.Documents.ToListAsync();
    }
}
