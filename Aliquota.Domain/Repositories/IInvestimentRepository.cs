using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Repositories;

public interface IInvestimentRepository
{
    public Task<IEnumerable<Investiment>> GetAsync();
    public Task CreateAsync(Investiment investiment);
}