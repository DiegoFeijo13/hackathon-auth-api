using Core.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FuncionarioRepository(AppDbContext dbContext) : IFuncionarioRepository
{
    public async Task<IEnumerable<FuncionarioEntity>> ListarFuncionarios() =>
        await dbContext.Funcionarios.ToListAsync();
    

    public async Task<FuncionarioEntity> ObterFuncionarioPorIdAsync(Guid id) =>
        await dbContext.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

    public async Task<FuncionarioEntity> CriarFuncionarioAsync(FuncionarioEntity funcionario)
    {
        funcionario.Id = Guid.NewGuid();
        dbContext.Funcionarios.Add(funcionario);
        
        await dbContext.SaveChangesAsync();
        return funcionario;
    }

    public async Task<bool> DeletarFuncionarioAsync(Guid id)
    {
        var funcionario = await dbContext.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

        if (funcionario is null)
            return false;
        
        dbContext.Funcionarios.Remove(funcionario);
        return await dbContext.SaveChangesAsync() > 0;
    }
}