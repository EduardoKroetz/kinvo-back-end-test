namespace Aliquota.Domain.DTOs.Investment;

public record CreateInvestmentDto(int FinancialProductId, DateTime RedeemIn, decimal Amount);