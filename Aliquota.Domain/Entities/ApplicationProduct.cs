namespace Aliquota.Domain.Entities;

public class ApplicationProduct
{
    public ApplicationProduct(FinancialProduct financialProduct, DateTime appliedIn, DateTime redeemIn)
    {
        FinancialProduct = financialProduct;
        ProfitabilityPerYear = financialProduct.ProfitabilityPerYear;
        AppliedIn = appliedIn;
        RedeemIn = redeemIn;
    }

    public FinancialProduct FinancialProduct { get; private set; }
    public decimal ProfitabilityPerYear { get; private set; }
    public DateTime AppliedIn { get; private set; }
    public DateTime RedeemIn { get; private set; }
    public decimal GrossProfit { get => GrossRedeem(); }
    public decimal Profit { get => Redeem(); }
    public TimeSpan ApplicationTime { get => RedeemIn - AppliedIn; }

    private decimal Redeem()
    {
        decimal profitabilityPerHour = ProfitabilityPerYear / 8.760m;
        var profit = profitabilityPerHour * (decimal) ApplicationTime.TotalHours;
        return profit;
    }

    private decimal GrossRedeem()
    {
        decimal grossProfit = 0;
        decimal onePercent = ( Profit / 100 );
        if (ApplicationTime.TotalDays < 365) //Menos de um ano de aplicação
        {
            grossProfit = Profit - (onePercent * AliquotaTable.OneYear);
        }else if (ApplicationTime.TotalDays >= 365 && ApplicationTime.TotalDays < 730) //De 1 a 2 anos de aplicação
        {
            grossProfit = Profit - (onePercent * AliquotaTable.TwoYear);
        }
        else //Mais de 2 anos de aplicação
        {
            grossProfit = Profit - (onePercent * AliquotaTable.AboveTwoYear);
        }

        return grossProfit;
    }
}
