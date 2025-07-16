using GradTest.Utils;
using Hangfire;

namespace GradTest.Configuration;

public static class JobConfiguration
{
    public static void SetupJobs(this IApplicationBuilder app)
    {
        var jobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();

        RecurringJob.AddOrUpdate<IExchangeRateSyncJob>(
            "sync-exchange-rate",
            job => job.SyncAndStoreAsync(),
            "0 */2 * * *"
        );
        
    }  
}