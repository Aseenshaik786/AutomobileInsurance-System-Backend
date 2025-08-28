using AutomobileInsuranceSystem.Models.DTOs;

namespace AutomobileInsuranceSystem.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(TokenUser user);
    }

}