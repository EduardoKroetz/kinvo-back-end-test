using Aliquota.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aliquota.Infra.Persistance;

public class AliquotaDbContext : DbContext
{
    public DbSet<FinancialProduct> FinancialProducts { get; set; }
    public DbSet<Investiment> Investiments { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("AliquotaMemoryDatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FinancialProduct>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Investiment>()
            .HasKey(x => x.Id);
    }
}