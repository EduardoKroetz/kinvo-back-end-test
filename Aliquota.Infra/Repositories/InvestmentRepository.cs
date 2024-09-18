
using Aliquota.Domain.Entities;
using Aliquota.Domain.Repositories;
using Aliquota.Infra.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Aliquota.Infra.Repositories;

public class InvestmentRepository : IInvestmentRepository
{
    private readonly AliquotaDbContext _dbContext;

    public InvestmentRepository(AliquotaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Investment investment)
    {
        await _dbContext.Investments.AddAsync(investment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Investment>> GetAsync()
    {
        return await _dbContext.Investments.ToListAsync();
    }
}
