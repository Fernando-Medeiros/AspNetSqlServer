using Microsoft.EntityFrameworkCore;

namespace Persistence.Models;

// Principal
public class Universidade
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Collection navigation containing dependents
    public ICollection<Curso> Cursos { get; } = [];
    public ICollection<Aluno> Alunos { get; } = [];
    public ICollection<Instrutor> Instrutores { get; } = [];

    internal static void OnCreating(ModelBuilder builder)
    {
        builder.Entity<Universidade>()
           .HasKey(x => x.Id);

        builder.Entity<Universidade>()
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<Universidade>()
            .HasMany(x => x.Alunos)
            .WithOne(x => x.Universidade)
            .HasForeignKey(x => x.UniversidadeId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Entity<Universidade>()
            .HasMany(x => x.Cursos)
            .WithOne(x => x.Universidade)
            .HasForeignKey(x => x.UniversidadeId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Entity<Universidade>()
             .HasMany(x => x.Instrutores)
             .WithOne(x => x.Universidade)
             .HasForeignKey(x => x.UniversidadeId)
             .HasPrincipalKey(x => x.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired();
    }
}

