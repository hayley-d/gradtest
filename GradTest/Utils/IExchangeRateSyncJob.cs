namespace GradTest.Utils;

public interface IExchangeRateSyncJob
{
    Task SyncAndStoreAsync();
}