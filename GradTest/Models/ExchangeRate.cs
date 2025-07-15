namespace GradTest.Models;

public class ExchangeRate
{
    public Guid Id { get; private set; } =  Guid.NewGuid();
    public decimal USD { get; set; } = 1;
    public decimal ZAR { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public ExchangeRate(decimal zar)
    {
        ZAR = zar;
    }
}