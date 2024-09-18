using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Tests.Entities;

public class InvestmentTests
{
    private readonly FinancialProduct _financialProduct = new("Tesouro Selic 2029", 10.0m);
    
    [Fact]
    public void CreateInvestment_WithAmountLess0_ReturnException()
    {
        Assert.Throws<ArgumentException>(() => new Investment(_financialProduct, DateTime.Now.AddYears(2), 0));
    }

    [Fact]
    public void CreateInvestment_WithRedeemDateBeforeAppliedDate_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Investment(_financialProduct, DateTime.Now.AddDays(-1), 1000));
        Assert.Equal("The redeem date cannot be earlier than the current date", exception.Message);
    }


    [Fact]
    public void InvestmentOf1000_WithAReturnOf10Percent_For1Year_ShouldHaveAReturnOf100_AndARealProfitOf77_5()
    {
        var amount = 1000;
        var financialProduct = new FinancialProduct("Tesouro Selic 2026", 10);
        var investment = new Investment(financialProduct, DateTime.Now.AddYears(1), amount);


        Assert.Equal(amount, investment.AmountApplied);
        Assert.Equal(100, Math.Round(investment.Profit, 2));
        Assert.Equal(77.50m, Math.Round(investment.RealProfit, 2)); //22,5% de imposto
    }

    [Fact]
    public void Investment_With10PercentProfit_For2Years_ShouldReturnCorrectProfitAndTax()
    {
        var amount = 1000;
        var investment = new Investment(_financialProduct, DateTime.Now.AddYears(2), amount);

        Assert.Equal(200, Math.Round(investment.Profit, 2));
        Assert.Equal(163.00m, Math.Round(investment.RealProfit, 2)); // 18,5% de imposto
    }

    [Fact]
    public void Investment_With10PercentProfit_For3Years_ShouldReturnCorrectProfitAndTax()
    {
        var amount = 1000;
        var investment = new Investment(_financialProduct, DateTime.Now.AddYears(3), amount);

        Assert.Equal(300, Math.Round(investment.Profit, 2));
        Assert.Equal(255.00m, Math.Round(investment.RealProfit, 2)); // 15% de imposto
    }


}
