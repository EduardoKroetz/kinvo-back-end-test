using Aliquota.Domain.DTOs.Investment;
using Aliquota.Domain.Entities;
using Aliquota.Domain.Repositories;
using Aliquota.Domain.Services;

namespace Aliquota.Application.Services;

public class InvestmentService : IInvestmentService
{
    private readonly IInvestmentRepository _investmentRepository;
    private readonly IFinancialProductRepository _financialProductRepository;

    public InvestmentService(IInvestmentRepository investmentRepository, IFinancialProductRepository financialProductRepository)
    {
        _investmentRepository = investmentRepository;
        _financialProductRepository = financialProductRepository;
    }

    public async Task CreateInvestment(CreateInvestmentDto createDto)
    {
        var financialProduct = await _financialProductRepository.GetByIdAsync(createDto.FinancialProductId);
        if (financialProduct == null)
        {
            throw new ArgumentException("Nenhum produto financeiro encontrado para o id " + createDto.FinancialProductId);
        }

        var investment = new Investment(financialProduct, createDto.RedeemIn, createDto.Amount);
        await _investmentRepository.CreateAsync(investment);
    }

    public async Task<IEnumerable<Investment>> GetInvestments()
    {
        return await _investmentRepository.GetAsync();
    }
}
