using Aliquota.Domain.DTOs.FinancialProduct;
using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Services;

public interface IFinancialProductService
{
    Task<IEnumerable<FinancialProduct>> GetFinancialProducts();
    Task CreateFinancialProduct(CreateFinancialProductDto createDto);
}