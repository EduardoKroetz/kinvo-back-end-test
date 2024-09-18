
using Aliquota.Application.Services;
using Aliquota.Domain.DTOs.FinancialProduct;
using Aliquota.Domain.DTOs.Investiment;
using Aliquota.Domain.Entities;
using Aliquota.Infra.Persistance;
using Aliquota.Infra.Repositories;

var dbContext = new AliquotaDbContext();
var financialProductRepository = new FinancialProductRepository(dbContext);
var financialProductService = new FinancialProductService(financialProductRepository);
var investimentRepository = new InvestimentRepository(dbContext);
var investimentService = new InvestimentService(investimentRepository, financialProductRepository);

Menu();

void Menu()
{
    Console.Clear();
    Console.WriteLine("1 - Listar Produtos financeiros");
    Console.WriteLine("2 - Listar Investimentos");
    Console.WriteLine("3 - Criar Produto financeiro (Campos: Nome do produto, Rentabilidade por ano)");
    Console.WriteLine("4 - Criar Investimento (Campos: Id do Produto financeiro, data de resgate, valor do investimento)");
    Console.WriteLine("5 - Sair");

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            ShowFinancialProduts();
            break;
        case "2":
            ShowInvestiments();
            break;
        case "3":
            CreateFinancialProduct();
            break;
        case "4":
            CreateInvestiment();
            break;
        case "5":
            System.Environment.Exit(0);
            break;
        default:
            Console.Clear();
            Console.WriteLine("Opção não encontrada");
            Task.Delay(1500);
            break;
    }

    Menu();
}

void ShowFinancialProduts()
{
    var financialProducts = financialProductService.GetFinancialProducts().Result;
    foreach(var financialProduct in financialProducts)
    {
        ShowFinancialProduct(financialProduct);
    }
    Console.ReadKey();
}
void ShowFinancialProduct(FinancialProduct financialProduct)
{
    Console.WriteLine($"\t{financialProduct.Id} | {financialProduct.Name} - Rentabilidade: {financialProduct.ProfitabilityPerYear}%");
}

void ShowInvestiments()
{
    var investiments = investimentService.GetInvestiments().Result;
    foreach (var investiment in investiments)
    {
        Console.WriteLine(@$"
        ID: {investiment.Id} | Aplicado em: {investiment.AppliedIn}
        Valor aplicado: {investiment.AmountApplied}    
        Rentabilidade: {investiment.ProfitabilityPerYear}% a.a
        Lucro Previsto: R${investiment.Profit} 
        Lucro Real(descontando IR): R${investiment.RealProfit}
        Produto Financeiro:" + "\n\t");

        ShowFinancialProduct(investiment.FinancialProduct);
    }
    Console.ReadKey();
}

void CreateFinancialProduct()
{
    try
    {
        Console.Clear();
        Console.WriteLine("Criar Produto Financeiro");

        Console.Write("Nome do produto: ");
        var name = Console.ReadLine();

        Console.Write("Rentabilidade por ano: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal profitabilityPerYear))
        {
            Console.WriteLine("Rentabilidade inválida. Deve ser um número decimal.");
            Task.Delay(1500).Wait();
            return;
        }

        var financialProduct = new CreateFinancialProductDto(name, profitabilityPerYear);
       
        financialProductService.CreateFinancialProduct(financialProduct).Wait();

        Console.WriteLine("Produto financeiro criado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocorreu um erro ao criar o produto financeiro: {ex.Message}");
    }
    finally
    {
        Task.Delay(1500).Wait();
        Menu();
    }
}


void CreateInvestiment()
{
    try
    {
        Console.Clear();
        Console.WriteLine("Criar Investimento");

        Console.Write("Id do Produto financeiro: ");
        if (!int.TryParse(Console.ReadLine(), out int financialProductId))
        {
            Console.WriteLine("ID inválido. Deve ser um número inteiro.");
            Task.Delay(1500).Wait();
            return;
        }

        Console.Write("Data de resgate (formato: dd/MM/yyyy): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime redemptionDate))
        {
            Console.WriteLine("Data inválida.");
            Task.Delay(1500).Wait();
            return;
        }

        Console.Write("Valor do investimento: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amountApplied))
        {
            Console.WriteLine("Valor inválido. Deve ser um número decimal.");
            Task.Delay(1500).Wait();
            return;
        }

        var investiment = new CreateInvestimentDto(financialProductId, redemptionDate, amountApplied);

        investimentService.CreateInvestiment(investiment).Wait();

        Console.WriteLine("Investimento criado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocorreu um erro ao criar o investimento: {ex.Message}");
    }
    finally
    {
        Task.Delay(1500).Wait();
        Menu();
    }
}
