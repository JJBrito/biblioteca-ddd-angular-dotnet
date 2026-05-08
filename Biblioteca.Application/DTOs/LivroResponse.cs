namespace Biblioteca.Application.DTOs;

public class LivroResponse
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public string Genero { get; set; } = string.Empty;

    public int AnoPublicacao { get; set; }

    public bool Disponivel { get; set; }

    public DateTime CriadoEm { get; set; }
}