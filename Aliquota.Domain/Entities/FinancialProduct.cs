namespace Aliquota.Domain.Entities;

public class FinancialProduct
{
    internal FinancialProduct() { }

    public FinancialProduct(string name, decimal profitabilityPerYear)
    {
        Name = name;
        ProfitabilityPerYear = profitabilityPerYear;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal ProfitabilityPerYear { get; private set; }
}
