using GradTest.Utils;
using Hangfire;

namespace GradTest.API.Configuration.App;

public static class JobConfiguration
{
    public static void SetupJobs(this IApplicationBuilder app)
    {
        var jobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();

        jobManager.AddOrUpdate<IExchangeRateSyncJob>(
            "sync-exchange-rate",
            job => job.SyncAndStoreAsync(),
            "0 */2 * * *"
        );
    }  
}