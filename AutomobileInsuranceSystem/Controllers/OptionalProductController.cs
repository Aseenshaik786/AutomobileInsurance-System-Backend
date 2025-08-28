using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutomobileInsuranceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Officer")] // Only Admin can add/delete
    public class OptionalProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OptionalProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OptionalProducts
        [HttpGet]
        [AllowAnonymous] // Publicly viewable
        public async Task<IActionResult> GetOptionalProducts()
        {
            var products = await _context.OptionalProducts.ToListAsync();
            return Ok(products);
        }

        // POST: api/OptionalProducts
        [HttpPost]
        public async Task<IActionResult> AddOptionalProduct([FromBody] OptionalProductDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Optional product name is required.");

            var optionalProduct = new OptionalProduct
            {
                Name = dto.Name
            };

            _context.OptionalProducts.Add(optionalProduct);
            await _context.SaveChangesAsync();

            return Ok(optionalProduct);
        }

        // DELETE: api/OptionalProducts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOptionalProduct(int id)
        {
            var product = await _context.OptionalProducts.FindAsync(id);
            if (product == null) return NotFound();

            _context.OptionalProducts.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
