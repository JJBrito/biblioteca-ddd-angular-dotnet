using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly BibliotecaDbContext _context;

    public LivroRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Livro>> ObterTodosAsync()
    {
        return await _context.Livros
            .OrderBy(x => x.Titulo)
            .ToListAsync();
    }

    public async Task<Livro?> ObterPorIdAsync(Guid id)
    {
        return await _context.Livros
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Livro>> FiltrarAsync(
        string? titulo,
        string? autor,
        string? genero)
    {
        var query = _context.Livros.AsQueryable();

        if (!string.IsNullOrWhiteSpace(titulo))
        {
            query = query.Where(x =>
                x.Titulo.Contains(titulo));
        }

        if (!string.IsNullOrWhiteSpace(autor))
        {
            query = query.Where(x =>
                x.Autor.Contains(autor));
        }

        if (!string.IsNullOrWhiteSpace(genero))
        {
            query = query.Where(x =>
                x.Genero.Contains(genero));
        }

        return await query
            .OrderBy(x => x.Titulo)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Livro livro)
    {
        await _context.Livros.AddAsync(livro);
    }

    public void Atualizar(Livro livro)
    {
        _context.Livros.Update(livro);
    }

    public void Remover(Livro livro)
    {
        _context.Livros.Remove(livro);
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}