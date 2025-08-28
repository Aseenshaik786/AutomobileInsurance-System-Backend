using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Interfaces;
using AutomobileInsuranceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly AppDbContext _context;

        public PolicyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Policy>> GetAllPoliciesAsync()
        {
            return await _context.Policies.ToListAsync();
        }

        public async Task<Policy> GetPolicyByIdAsync(int policyId)
        {
            return await _context.Policies.FindAsync(policyId);
        }

        public async Task<Policy> AddPolicyAsync(Policy policy)
        {
            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();
            return policy;
        }
    }
}
