namespace Biblioteca.Application.DTOs;

public class CriarLivroRequest
{
    public string Titulo { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public string Genero { get; set; } = string.Empty;

    public int AnoPublicacao { get; set; }
}