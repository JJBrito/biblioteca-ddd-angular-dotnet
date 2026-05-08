using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly ILivroService _livroService;

    public LivrosController(ILivroService livroService)
    {
        _livroService = livroService;
    }

    [HttpGet]
    public async Task<ActionResult<List<LivroResponse>>> ObterTodos()
    {
        var livros = await _livroService.ObterTodosAsync();

        return Ok(livros);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LivroResponse>> ObterPorId(Guid id)
    {
        var livro = await _livroService.ObterPorIdAsync(id);

        if (livro == null)
            return NotFound();

        return Ok(livro);
    }

    [HttpGet("filtrar")]
    public async Task<ActionResult<List<LivroResponse>>> Filtrar(
        [FromQuery] string? titulo,
        [FromQuery] string? autor,
        [FromQuery] string? genero)
    {
        var livros = await _livroService.FiltrarAsync(titulo, autor, genero);

        return Ok(livros);
    }

    [HttpPost]
    public async Task<ActionResult<LivroResponse>> Criar(
        [FromBody] CriarLivroRequest request)
    {
        var livro = await _livroService.CriarAsync(request);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = livro.Id },
            livro);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(
        Guid id,
        [FromBody] AtualizarLivroRequest request)
    {
        var atualizado = await _livroService.AtualizarAsync(id, request);

        if (!atualizado)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var removido = await _livroService.RemoverAsync(id);

        if (!removido)
            return NotFound();

        return NoContent();
    }
}