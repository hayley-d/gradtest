using GradTest.Domain.Entities;

namespace GradTest.Services;

public interface IExchangeRateService
{
    Task<ExchangeRate?> GetExchangeRateAsync();
}