using Aliquota.Domain.DTOs.Investiment;
using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Services;

public interface IInvestimentService
{
    Task<IEnumerable<Investiment>> GetInvestiments();
    Task CreateInvestiment(CreateInvestimentDto createDto);
}