using GradTest.Utils;
using Hangfire;

namespace GradTest.Configuration;

public static class JobConfiguration
{
    public static void SetupJobs(this WebApplicationBuilder builder)
    {
        RecurringJob.AddOrUpdate<IExchangeRateSyncJob>(
            "sync-exchange-rate",
            job => job.SyncAndStoreAsync(),
            "0 */2 * * *"
            );
    }  
}