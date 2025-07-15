using GradTest.Models;

namespace GradTest.Services;



public class ExchangeRateService: IExchangeRateService
{
   private class OpenExchangeResponse
   {
      public Dictionary<string, decimal> Rates { get; set; }
   }
   
   private readonly HttpClient _client;
   private readonly IConfiguration _config;
   private readonly ILogger<ExchangeRateService> _logger;

   public ExchangeRateService(HttpClient client, IConfiguration config, ILogger<ExchangeRateService> logger)
   {
      _client = client;
      _config = config;
      _logger = logger;
   }

   public async Task<ExchangeRate?> GetExchangeRateAsync()
   {
      try
      {
         var apiKey = _config["OpenExchangeRates:ApiKey"];

         if (string.IsNullOrWhiteSpace(apiKey))
         {
            _logger.LogError("API key for Open Exchange Rates not found.");
            return null;
         }

         var url = $"https://openexchangerates.org/api/latest.json?app_id={apiKey}&symbols=ZAR,USD";

         var httpResponse = await _client.GetAsync(url);

         if (!httpResponse.IsSuccessStatusCode)
         {
            _logger.LogError($"Failed to fetch exchange rates: {httpResponse.StatusCode}");
            return null;
         }

         var response = await httpResponse.Content.ReadFromJsonAsync<OpenExchangeResponse>();

         if (response is null || !response.Rates.TryGetValue("ZAR", out var zar))
         {
            _logger.LogError($"Response if missing ZAR rate.");
            return null;
         }

         return new ExchangeRate(zar);
      }
      catch (HttpRequestException ex)
      {
         _logger.LogError($"Network error while fetching exchange rates: {ex.Message}");
         return null;
      }
      catch (Exception ex)
      {
         _logger.LogError($"Unexpected error while fetching exchange rates: {ex.Message}");
         return null;
      }
   }
}