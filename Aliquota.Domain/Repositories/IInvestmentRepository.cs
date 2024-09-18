using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Repositories;

public interface IInvestmentRepository
{
    public Task<IEnumerable<Investment>> GetAsync();
    public Task CreateAsync(Investment investment);
}