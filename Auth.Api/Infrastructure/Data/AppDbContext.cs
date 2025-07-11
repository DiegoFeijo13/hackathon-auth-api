using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<FuncionarioEntity> Funcionarios {get;set;}
    public DbSet<ClienteEntity> Clientes { get; set; }
}