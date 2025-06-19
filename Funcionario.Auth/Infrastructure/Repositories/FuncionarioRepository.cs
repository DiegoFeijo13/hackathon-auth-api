using Core.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FuncionarioRepository(AppDbContext dbContext) : IFuncionarioRepository
{
    private readonly PasswordHasher<object> _passwordHasher = new();
    
    public async Task<FuncionarioEntity> CriarFuncionarioAsync(FuncionarioEntity funcionario)
    {
        funcionario.Senha = _passwordHasher.HashPassword(null, funcionario.Senha);         
        
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

    public async Task<FuncionarioEntity?> GetAsync(string email, string senha)
    {
        var usuario = await dbContext.Funcionarios
            .AsNoTracking()
            .Where(f => f.Email == email)
            .FirstOrDefaultAsync();

        if (usuario is null)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(null, usuario.Senha, senha);

        if(result != PasswordVerificationResult.Success)
            return null;

        return usuario;

    }
}