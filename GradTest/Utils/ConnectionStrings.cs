namespace GradTest.Utils;

public static class ConnectionStrings
{
   public static string GetPostgresConnectionString()
   {
      DotNetEnv.Env.Load();
      var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
      var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
      var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

      if (string.IsNullOrWhiteSpace(db) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password) ||
          string.IsNullOrWhiteSpace(password))
          throw new InvalidOperationException("Missing required environment variables: POSTGRES_DB, POSTGRES_USER, POSTGRES_PASSWORD");  

      return $"Host=localhost;Database={db};Username={user};Password={password}";
   }

   public static string GetApiKey()
   {
       DotNetEnv.Env.Load();
       string apiKey = Environment.GetEnvironmentVariable("OPEN_EX_APIKEY") ?? throw new InvalidOperationException("Missing required environment variable: OPEN_EX_APIKEY");

       if (string.IsNullOrWhiteSpace(apiKey)) 
           throw new InvalidOperationException("Missing required environment variable: OPEN_EX_APIKEY");

       return apiKey;
   }
}