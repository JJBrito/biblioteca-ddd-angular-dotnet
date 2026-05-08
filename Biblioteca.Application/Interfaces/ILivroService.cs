using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface ILivroService
{
    Task<List<LivroResponse>> ObterTodosAsync();

    Task<LivroResponse?> ObterPorIdAsync(Guid id);

    Task<List<LivroResponse>> FiltrarAsync(
        string? titulo,
        string? autor,
        string? genero);

    Task<LivroResponse> CriarAsync(CriarLivroRequest request);

    Task<bool> AtualizarAsync(Guid id, AtualizarLivroRequest request);

    Task<bool> RemoverAsync(Guid id);
}