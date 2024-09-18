using Aliquota.Domain.DTOs.Investiment;
using Aliquota.Domain.Entities;
using Aliquota.Domain.Repositories;
using Aliquota.Domain.Services;

namespace Aliquota.Application.Services;

public class InvestimentService : IInvestimentService
{
    private readonly IInvestimentRepository _investimentRepository;
    private readonly IFinancialProductRepository _financialProductRepository;

    public InvestimentService(IInvestimentRepository investimentRepository, IFinancialProductRepository financialProductRepository)
    {
        _investimentRepository = investimentRepository;
        _financialProductRepository = financialProductRepository;
    }

    public async Task CreateInvestiment(CreateInvestimentDto createDto)
    {
        var financialProduct = await _financialProductRepository.GetByIdAsync(createDto.FinancialProductId);
        if (financialProduct == null)
        {
            throw new ArgumentException("Nenhum produto financeiro encontrado para o id " + createDto.FinancialProductId);
        }

        var investiment = new Investiment(financialProduct, createDto.RedeemIn, createDto.Amount);
        await _investimentRepository.CreateAsync(investiment);
    }

    public async Task<IEnumerable<Investiment>> GetInvestiments()
    {
        return await _investimentRepository.GetAsync();
    }
}
