using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Repositories;

public interface IFinancialProductRepository
{
    public Task<IEnumerable<FinancialProduct>> GetAsync();
    public Task CreateAsync(FinancialProduct financialProduct);
    public Task<FinancialProduct?> GetByIdAsync(int id);

}