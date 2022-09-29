namespace ProgrammingWithPalermo.ChurchBulletin.UI.Server;

public class Is64BitProcessHealthCheck : IHealthCheck
{
    private readonly ILogger<Is64BitProcessHealthCheck> _logger;

    public Is64BitProcessHealthCheck(ILogger<Is64BitProcessHealthCheck> logger)
    {
        _logger = logger;
    }
    
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation($"Health check success");
        if (!Environment.Is64BitProcess)
        {
            return Task.FromResult(HealthCheckResult.Degraded());
        }

        return Task.FromResult(HealthCheckResult.Healthy());
    }
}