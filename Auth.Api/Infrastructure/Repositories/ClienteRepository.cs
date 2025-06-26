using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ClienteRepository(AppDbContext dbContext) : IClienteRepository
{
    private readonly PasswordHasher<object> _passwordHasher = new();
    
    public async Task<ClienteEntity> InsertAsync(ClienteEntity entity)
    {
        entity.Senha = _passwordHasher.HashPassword(null, entity.Senha);     
        entity.DataCriacao = DateTime.UtcNow;
        entity.DataAtualizacao = DateTime.UtcNow;
        
        dbContext.Clientes.Add(entity);        
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var cliente = await dbContext.Clientes.FirstOrDefaultAsync(f => f.Id == id);

        if (cliente is null)
            return false;
        
        dbContext.Clientes.Remove(cliente);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<ClienteEntity?> GetAsync(string cpf, string senha)
    {
        var usuario = await dbContext.Clientes
            .AsNoTracking()
            .Where(c => c.Cpf == cpf)
            .FirstOrDefaultAsync();

        if (usuario is null)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(null, usuario.Senha, senha);

        if(result != PasswordVerificationResult.Success)
            return null;

        return usuario;

    }
}