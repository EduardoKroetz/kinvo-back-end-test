namespace Aliquota.Domain.Entities;

public class Investment
{
    internal Investment() { }
    public Investment(FinancialProduct financialProduct, DateTime redeemIn, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("The amount invested cannot be less than 0");

        if (redeemIn < DateTime.Now)
            throw new ArgumentException("The redeem date cannot be earlier than the current date");

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
    public decimal RealProfit { get => CalculeNetProfit(); }
    public decimal Profit { get => CalculeProfit(); }
    public TimeSpan ApplicationTime { get => RedeemIn - AppliedIn; }

    private decimal CalculeProfit()
    {
        decimal profitabilityPerDay = ProfitabilityPerYear / 365; //Pega a rentabilidade por dia
        var profitability = ( profitabilityPerDay * (decimal)ApplicationTime.TotalDays );
        var profit = AmountApplied * (profitability / 100); 
        return profit;
    }

    private decimal CalculeNetProfit()
    {
        decimal realProfit = 0;
        var profit = Profit;
        decimal onePercent = ( profit / 100 );
        double totalDays = ApplicationTime.TotalDays; 

        if (totalDays < 365) //Menos de um ano de aplicação 
            realProfit = profit - (onePercent * AliquotaTable.OneYear);    
        else if (totalDays >= 365 && totalDays < 730) //De 1 a 2 anos de aplicação
            realProfit = profit - (onePercent * AliquotaTable.TwoYear);
        else //Mais de 2 anos de aplicação
            realProfit = profit - (onePercent * AliquotaTable.AboveTwoYear);
        

        return realProfit;
    }
}
