using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

public interface ILivroRepository
{
    Task<List<Livro>> ObterTodosAsync();

    Task<Livro?> ObterPorIdAsync(Guid id);

    Task<List<Livro>> FiltrarAsync(
        string? titulo,
        string? autor,
        string? genero);

    Task AdicionarAsync(Livro livro);

    void Atualizar(Livro livro);

    void Remover(Livro livro);

    Task SalvarAlteracoesAsync();
}