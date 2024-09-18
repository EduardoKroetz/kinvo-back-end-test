
using Aliquota.Application.Services;
using Aliquota.Domain.DTOs.FinancialProduct;
using Aliquota.Domain.DTOs.Investment;
using Aliquota.Domain.Entities;
using Aliquota.Infra.Persistance;
using Aliquota.Infra.Repositories;

var dbContext = new AliquotaDbContext();
var financialProductRepository = new FinancialProductRepository(dbContext);
var financialProductService = new FinancialProductService(financialProductRepository);
var investmentRepository = new InvestmentRepository(dbContext);
var investmentService = new InvestmentService(investmentRepository, financialProductRepository);

Menu();

void Menu()
{
    Console.Clear();
    Console.WriteLine("1 - Listar Produtos financeiros");
    Console.WriteLine("2 - Listar Investmentos");
    Console.WriteLine("3 - Criar Produto financeiro (Campos: Nome do produto, Rentabilidade por ano)");
    Console.WriteLine("4 - Criar Investmento (Campos: Id do Produto financeiro, data de resgate, valor do investmento)");
    Console.WriteLine("5 - Sair");

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            ShowFinancialProduts();
            break;
        case "2":
            ShowInvestments();
            break;
        case "3":
            CreateFinancialProduct();
            break;
        case "4":
            CreateInvestment();
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

void ShowInvestments()
{
    var investments = investmentService.GetInvestments().Result;
    foreach (var investment in investments)
    {
        Console.WriteLine(@$"
        ID: {investment.Id} | Aplicado em: {investment.AppliedIn}
        Valor aplicado: {investment.AmountApplied}    
        Rentabilidade: {investment.ProfitabilityPerYear}% a.a
        Lucro Previsto: R${investment.Profit} 
        Lucro Real(descontando IR): R${investment.RealProfit}
        Produto Financeiro:" + "\n\t");

        ShowFinancialProduct(investment.FinancialProduct);
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


void CreateInvestment()
{
    try
    {
        Console.Clear();
        Console.WriteLine("Criar Investmento");

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

        Console.Write("Valor do investmento: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amountApplied))
        {
            Console.WriteLine("Valor inválido. Deve ser um número decimal.");
            Task.Delay(1500).Wait();
            return;
        }

        var investment = new CreateInvestmentDto(financialProductId, redemptionDate, amountApplied);

        investmentService.CreateInvestment(investment).Wait();

        Console.WriteLine("Investmento criado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocorreu um erro ao criar o investmento: {ex.Message}");
    }
    finally
    {
        Task.Delay(1500).Wait();
        Menu();
    }
}
