namespace GradTest.API.Configuration.Builder;

public static class LoggingConfiguration
{
    public static void SetupLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
    } 
}