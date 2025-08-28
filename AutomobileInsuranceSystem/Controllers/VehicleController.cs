// Controllers/VehiclesController.cs
using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AutomobileInsuranceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public VehiclesController(AppDbContext context) { _context = context; }

        // Get vehicles for current user
        [HttpGet("my")]
        public async Task<IActionResult> GetMyVehicles()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("User ID not found in token.");
            int userId = int.Parse(userIdClaim);

            var vehicles = await _context.Vehicles.Where(v => v.UserId == userId).ToListAsync();
            return Ok(vehicles);
        }

        // Add vehicle for current user
        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleDTO dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("User ID not found in token.");
            int userId = int.Parse(userIdClaim);

            var vehicle = new Vehicle
            {
                UserId = userId,
                VehicleType = dto.VehicleType,
                Make = dto.Make,
                EnginePerformanceKW = dto.EnginePerformanceKW,
                DateOfManufacture = dto.DateOfManufacture,
                NumberOfSeats = dto.NumberOfSeats,
                FuelType = dto.FuelType,
                ListPrice = dto.ListPrice,
                LicensePlateNumber = dto.LicensePlateNumber,
                AnnualMileage = dto.AnnualMileage
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return Ok(vehicle);
        }
    }
}
