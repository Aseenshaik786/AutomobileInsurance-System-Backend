// Controllers/PoliciesController.cs
using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PoliciesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PoliciesController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var policies = await _context.Policies.ToListAsync();
            return Ok(policies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null) return NotFound();
            return Ok(policy);
        }

        [Authorize(Roles = "Officer,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PolicyDTO dto)
        {
            var p = new Policy
            {
                Name = dto.Name,
                Description = dto.Description,
                BasePremium = dto.BasePremium,
                DurationMonths = dto.DurationMonths,
                AddOns = dto.AddOns
            };
            _context.Policies.Add(p);
            await _context.SaveChangesAsync();
            return Ok(p);
        }
    }
}
