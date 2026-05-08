namespace Biblioteca.Domain.Entities;

public class Livro
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public string Genero { get; private set; }
    public int AnoPublicacao { get; private set; }
    public bool Disponivel { get; private set; }
    public DateTime CriadoEm { get; private set; }

    private Livro() { }

    public Livro(string titulo, string autor, string genero, int anoPublicacao)
    {
        Validar(titulo, autor, genero, anoPublicacao);

        Id = Guid.NewGuid();
        Titulo = titulo.Trim();
        Autor = autor.Trim();
        Genero = genero.Trim();
        AnoPublicacao = anoPublicacao;
        Disponivel = true;
        CriadoEm = DateTime.UtcNow;
    }

    public void Atualizar(string titulo, string autor, string genero, int anoPublicacao)
    {
        Validar(titulo, autor, genero, anoPublicacao);

        Titulo = titulo.Trim();
        Autor = autor.Trim();
        Genero = genero.Trim();
        AnoPublicacao = anoPublicacao;
    }

    public void MarcarComoDisponivel()
    {
        Disponivel = true;
    }

    public void MarcarComoIndisponivel()
    {
        Disponivel = false;
    }

    private static void Validar(string titulo, string autor, string genero, int anoPublicacao)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título do livro é obrigatório.");

        if (string.IsNullOrWhiteSpace(autor))
            throw new ArgumentException("O autor do livro é obrigatório.");

        if (string.IsNullOrWhiteSpace(genero))
            throw new ArgumentException("O gênero do livro é obrigatório.");

        if (anoPublicacao <= 0)
            throw new ArgumentException("O ano de publicação deve ser válido.");

        if (anoPublicacao > DateTime.UtcNow.Year)
            throw new ArgumentException("O ano de publicação não pode ser futuro.");
    }
}