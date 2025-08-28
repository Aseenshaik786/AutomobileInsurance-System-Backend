using AutomobileInsuranceSystem.Models.DTOs;

namespace AutomobileInsuranceSystem.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDTO dto);
        Task<string> LoginAsync(LoginDTO dto);
    }
}
