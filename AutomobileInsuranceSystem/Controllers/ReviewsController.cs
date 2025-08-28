using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = await _context.Users.AnyAsync(u => u.UserId == dto.UserId);
            if (!userExists)
                return NotFound($"User with ID {dto.UserId} not found");

            var review = new Review
            {
                UserId = dto.UserId,
                Content = dto.Content,
                Rating = dto.Rating,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Return just the review with minimal user info
            var result = await _context.Reviews
                .Where(r => r.ReviewId == review.ReviewId)
                .Select(r => new
                {
                    r.ReviewId,
                    r.UserId,
                    r.Content,
                    r.Rating,
                    r.CreatedAt,
                    User = new
                    {
                        r.User.UserId,
                        r.User.FirstName,
                        r.User.LastName,
                        r.User.Email
                    }
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetReviewById), new { id = review.ReviewId }, result);
        }

        private object GetReviewById()
        {
            throw new NotImplementedException();
        }

        [HttpGet("user/{userId}/policy/{policyId}")]
        public async Task<IActionResult> GetReviewsByUserForPolicy(int userId, int policyId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.UserId == userId)
                .Where(r => _context.Proposals.Any(p => p.UserId == r.UserId && p.PolicyId == policyId))
                .Select(r => new
                {
                    r.ReviewId,
                    r.Content,
                    r.Rating,
                    r.CreatedAt,
                    User = new
                    {
                        r.User.UserId,
                        r.User.FirstName,
                        r.User.LastName,
                        r.User.Email
                    }
                })
                .ToListAsync();

            if (reviews == null || !reviews.Any())
                return NotFound("No reviews found for this user and policy.");

            return Ok(reviews);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Reviews.ToListAsync());
        }
    }
}
