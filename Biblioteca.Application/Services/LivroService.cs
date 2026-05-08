using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services;

public class LivroService : ILivroService
{
    private readonly ILivroRepository _livroRepository;

    public LivroService(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }

    public async Task<List<LivroResponse>> ObterTodosAsync()
    {
        var livros = await _livroRepository.ObterTodosAsync();

        return livros.Select(MapearParaResponse).ToList();
    }

    public async Task<LivroResponse?> ObterPorIdAsync(Guid id)
    {
        var livro = await _livroRepository.ObterPorIdAsync(id);

        if (livro == null)
            return null;

        return MapearParaResponse(livro);
    }

    public async Task<List<LivroResponse>> FiltrarAsync(
        string? titulo,
        string? autor,
        string? genero)
    {
        var livros = await _livroRepository.FiltrarAsync(
            titulo,
            autor,
            genero);

        return livros.Select(MapearParaResponse).ToList();
    }

    public async Task<LivroResponse> CriarAsync(CriarLivroRequest request)
    {
        var livro = new Livro(
            request.Titulo,
            request.Autor,
            request.Genero,
            request.AnoPublicacao);

        await _livroRepository.AdicionarAsync(livro);

        await _livroRepository.SalvarAlteracoesAsync();

        return MapearParaResponse(livro);
    }

    public async Task<bool> AtualizarAsync(
        Guid id,
        AtualizarLivroRequest request)
    {
        var livro = await _livroRepository.ObterPorIdAsync(id);

        if (livro == null)
            return false;

        livro.Atualizar(
            request.Titulo,
            request.Autor,
            request.Genero,
            request.AnoPublicacao);

        _livroRepository.Atualizar(livro);

        await _livroRepository.SalvarAlteracoesAsync();

        return true;
    }

    public async Task<bool> RemoverAsync(Guid id)
    {
        var livro = await _livroRepository.ObterPorIdAsync(id);

        if (livro == null)
            return false;

        _livroRepository.Remover(livro);

        await _livroRepository.SalvarAlteracoesAsync();

        return true;
    }

    private static LivroResponse MapearParaResponse(Livro livro)
    {
        return new LivroResponse
        {
            Id = livro.Id,
            Titulo = livro.Titulo,
            Autor = livro.Autor,
            Genero = livro.Genero,
            AnoPublicacao = livro.AnoPublicacao,
            Disponivel = livro.Disponivel,
            CriadoEm = livro.CriadoEm
        };
    }
}