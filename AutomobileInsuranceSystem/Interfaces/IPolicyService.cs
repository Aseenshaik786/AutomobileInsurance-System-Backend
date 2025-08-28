using AutomobileInsuranceSystem.Models;

namespace AutomobileInsuranceSystem.Interfaces
{
    public interface IPolicyService
    {
        Task<List<Policy>> GetAllPoliciesAsync();
        Task<Policy> GetPolicyByIdAsync(int policyId);
        Task<Policy> AddPolicyAsync(Policy policy);
    }
}
