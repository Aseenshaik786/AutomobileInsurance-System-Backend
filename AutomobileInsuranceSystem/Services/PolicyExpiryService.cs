// Services/PolicyExpiryService.cs
using AutomobileInsuranceSystem.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Services
{
    public class PolicyExpiryService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PolicyExpiryService> _logger;

        public PolicyExpiryService(IServiceScopeFactory scopeFactory, ILogger<PolicyExpiryService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("PolicyExpiryService started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var now = DateTime.UtcNow;
                    // active proposals where StartDate + policy duration months < now
                    var expired = await db.Proposals
                        .Include(p => p.Policy)
                        .Where(p => p.PolicyStatus == "Active" &&
                                    p.Policy != null &&
                                    p.StartDate.AddMonths(p.Policy.DurationMonths) < now)
                        .ToListAsync(stoppingToken);

                    if (expired.Any())
                    {
                        foreach (var p in expired)
                        {
                            p.PolicyStatus = "Expired";
                        }
                        await db.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation("Marked {count} proposals as Expired.", expired.Count);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in PolicyExpiryService");
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
