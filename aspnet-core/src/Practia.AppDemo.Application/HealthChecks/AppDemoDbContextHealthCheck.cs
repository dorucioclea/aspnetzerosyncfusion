using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Practia.AppDemo.EntityFrameworkCore;

namespace Practia.AppDemo.HealthChecks
{
    public class AppDemoDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public AppDemoDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("AppDemoDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("AppDemoDbContext could not connect to database"));
        }
    }
}
