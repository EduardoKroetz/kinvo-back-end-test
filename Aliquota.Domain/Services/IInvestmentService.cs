using Aliquota.Domain.DTOs.Investment;
using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Services;

public interface IInvestmentService
{
    Task<IEnumerable<Investment>> GetInvestments();
    Task CreateInvestment(CreateInvestmentDto createDto);
}