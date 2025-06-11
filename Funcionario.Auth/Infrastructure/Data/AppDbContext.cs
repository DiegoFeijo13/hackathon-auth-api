using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext
{
    public DbSet<FuncionarioEntity> Funcionarios {get;set;}
}