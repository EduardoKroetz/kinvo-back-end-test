
using Aliquota.Domain.Entities;
using Aliquota.Domain.Repositories;
using Aliquota.Infra.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Aliquota.Infra.Repositories;

public class InvestimentRepository : IInvestimentRepository
{
    private readonly AliquotaDbContext _dbContext;

    public InvestimentRepository(AliquotaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Investiment investiment)
    {
        await _dbContext.Investiments.AddAsync(investiment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Investiment>> GetAsync()
    {
        return await _dbContext.Investiments.ToListAsync();
    }
}
