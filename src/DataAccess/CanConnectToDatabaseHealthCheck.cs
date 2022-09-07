using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

namespace ProgrammingWithPalermo.ChurchBulletin.DataAccess;

public class CanConnectToDatabaseHealthCheck : IHealthCheck
{
    private readonly ILogger<CanConnectToDatabaseHealthCheck> _logger;
    private readonly DataContext _context;

    public CanConnectToDatabaseHealthCheck(ILogger<CanConnectToDatabaseHealthCheck> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        if (!_context.Database.CanConnect()) 
            return Task.FromResult(HealthCheckResult.Unhealthy("Cannot connect to database"));
        
        _logger.LogInformation($"Health check success");
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}