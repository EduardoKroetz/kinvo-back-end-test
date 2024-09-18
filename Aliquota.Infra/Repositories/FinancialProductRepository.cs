using Aliquota.Domain.Entities;
using Aliquota.Domain.Repositories;
using Aliquota.Infra.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Aliquota.Infra.Repositories;

public class FinancialProductRepository : IFinancialProductRepository
{
    private readonly AliquotaDbContext _dbContext;

    public FinancialProductRepository(AliquotaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(FinancialProduct financialProduct)
    {
        await _dbContext.FinancialProducts.AddAsync(financialProduct);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<FinancialProduct>> GetAsync()
    {
        return await _dbContext.FinancialProducts.ToListAsync();
    }

    public async Task<FinancialProduct?> GetByIdAsync(int id)
    {
        return await _dbContext.FinancialProducts.FirstOrDefaultAsync(x => x.Id == id);
    }
}