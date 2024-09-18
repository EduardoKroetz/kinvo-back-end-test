namespace Aliquota.Domain.Entities;

public class Investiment
{
    internal Investiment() { }
    public Investiment(FinancialProduct financialProduct, DateTime redeemIn, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("The amount invested cannot be less than 0");

        FinancialProduct = financialProduct;
        ProfitabilityPerYear = financialProduct.ProfitabilityPerYear;
        AppliedIn = DateTime.Now;
        RedeemIn = redeemIn;
        AmountApplied = amount;
    }

    public int Id { get; private set; }
    public FinancialProduct FinancialProduct { get; private set; }
    public decimal ProfitabilityPerYear { get; private set; }
    public decimal AmountApplied { get; private set; }
    public DateTime AppliedIn { get; private set; }
    public DateTime RedeemIn { get; private set; }
    public decimal RealProfit { get => RealRedeem(); }
    public decimal Profit { get => Redeem(); }
    public TimeSpan ApplicationTime { get => RedeemIn - AppliedIn; }

    private decimal Redeem()
    {
        decimal profitabilityPerDay = ProfitabilityPerYear / 365; //Pega a rentabilidade por dia
        var profitability = ( profitabilityPerDay * (decimal)ApplicationTime.TotalDays );
        var profit = AmountApplied * (profitability / 100); 
        return profit;
    }

    private decimal RealRedeem()
    {
        decimal realProfit = 0;
        var profit = Profit; //Evitar recalculo para cada chamada da propriedade 'Profit'
        decimal onePercent = ( profit / 100 );
        if (ApplicationTime.TotalDays < 365) //Menos de um ano de aplicação
        {
            realProfit = profit - (onePercent * AliquotaTable.OneYear);
        }else if (ApplicationTime.TotalDays >= 365 && ApplicationTime.TotalDays < 730) //De 1 a 2 anos de aplicação
        {
            realProfit = profit - (onePercent * AliquotaTable.TwoYear);
        }
        else //Mais de 2 anos de aplicação
        {
            realProfit = profit - (onePercent * AliquotaTable.AboveTwoYear);
        }

        return realProfit;
    }
}
