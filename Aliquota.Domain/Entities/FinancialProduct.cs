namespace Aliquota.Domain.Entities;

public class FinancialProduct
{
    public FinancialProduct(string name, decimal profitabilityPerYear)
    {
        Name = name;
        ProfitabilityPerYear = profitabilityPerYear;
    }

    public string Name { get; private set; }
    public decimal ProfitabilityPerYear { get; private set; }
}
