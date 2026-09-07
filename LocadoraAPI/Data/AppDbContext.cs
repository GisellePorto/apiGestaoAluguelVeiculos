using Microsoft.EntityFrameworkCore;
using LocadoraAPI.Models;

namespace LocadoraAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<Aluguel> Alugueis => Set<Aluguel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.Cpf)
            .IsUnique();

        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<Veiculo>()
            .HasIndex(v => v.Placa)
            .IsUnique();
    }
}