using GradTest.Models;

namespace GradTest.Services;

public interface IExchangeRateService
{
    Task<ExchangeRate?> GetExchangeRateAsync();
}