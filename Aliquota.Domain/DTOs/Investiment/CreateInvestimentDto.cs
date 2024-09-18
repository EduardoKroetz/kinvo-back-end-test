namespace Aliquota.Domain.DTOs.Investiment;

public record CreateInvestimentDto(int FinancialProductId, DateTime RedeemIn, decimal Amount);