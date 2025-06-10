using Auth.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.DB;

public class FuncionarioDb : DbContext
{
    public FuncionarioDb(DbContextOptions<FuncionarioDb> options)
    : base(options) { }
    
    public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
}