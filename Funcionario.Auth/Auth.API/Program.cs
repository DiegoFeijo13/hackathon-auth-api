using Auth.API.DB;
using Auth.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FuncionarioDb>(opt => opt.UseInMemoryDatabase("Funcionarios"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
    c.SwaggerDoc("v1", new OpenApiInfo{ Title = "Auth.API", Version = "v1" })
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth.API v1")
);

app.MapGet("/funcionarios", async (FuncionarioDb db) => 
    await db.Funcionarios.ToListAsync());

app.MapGet("/funcionarios/{id}",async (Guid id, FuncionarioDb db) =>
    await db.Funcionarios.FindAsync(id)
        is { } funcionario
            ? Results.Ok(funcionario)
            : Results.NotFound());

//TODO: adicionar validadores
//TODO: geração automática de id
//TODO: criptografar senha
app.MapPost("/funcionarios", async (Funcionario funcionario, FuncionarioDb db) =>
{
    db.Funcionarios.Add(funcionario);
    await db.SaveChangesAsync();

    return Results.Created($"/funcionarios/{funcionario.Id}", funcionario);
});

app.MapPut("/funcionarios/{id}", async (Guid id, Funcionario inputFuncionario, FuncionarioDb db) =>
{
    var funcionario = await db.Funcionarios.FindAsync(id);

    if (funcionario is null)
        return Results.NotFound();

    funcionario.Nome = inputFuncionario.Nome;
    funcionario.Email = inputFuncionario.Email;
    funcionario.Senha = inputFuncionario.Senha;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/funcionarios/{id}", async (Guid id, FuncionarioDb db) =>
{
    if (await db.Funcionarios.FindAsync(id) is Funcionario funcionario)
    {
        db.Funcionarios.Remove(funcionario);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();