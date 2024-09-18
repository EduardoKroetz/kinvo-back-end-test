using Aliquota.Domain.DTOs.FinancialProduct;
using Aliquota.Domain.Entities;
using Aliquota.Domain.Repositories;
using Aliquota.Domain.Services;

namespace Aliquota.Application.Services;

public class FinancialProductService : IFinancialProductService
{
    private readonly IFinancialProductRepository _financialProductRepository;

    public FinancialProductService(IFinancialProductRepository financialProductRepository)
    {
        _financialProductRepository = financialProductRepository;
    }

    public async Task CreateFinancialProduct(CreateFinancialProductDto createDto)
    {
        var financialProduct = new FinancialProduct(createDto.Name, createDto.ProfitabilityPerYear);
        await _financialProductRepository.CreateAsync(financialProduct);
    }

    public async Task<IEnumerable<FinancialProduct>> GetFinancialProducts()
    {
        return await _financialProductRepository.GetAsync();
    }
}
