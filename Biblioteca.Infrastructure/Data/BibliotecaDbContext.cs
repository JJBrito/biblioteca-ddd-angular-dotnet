using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Data;

public class BibliotecaDbContext : DbContext
{
    public BibliotecaDbContext(
        DbContextOptions<BibliotecaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Livro> Livros => Set<Livro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Livro>(entity =>
        {
            entity.ToTable("Livros");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Autor)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Genero)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.AnoPublicacao)
                .IsRequired();

            entity.Property(x => x.Disponivel)
                .IsRequired();

            entity.Property(x => x.CriadoEm)
                .IsRequired();
        });
    }
}
