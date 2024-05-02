using Microsoft.EntityFrameworkCore;

namespace Persistence.Models;

// Dependente
public class Disciplina
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Muitos para muitos básico (N .. N)
    public ICollection<Curso> Cursos { get; } = [];
    public ICollection<Instrutor> Instrutores { get; } = [];

    internal static void OnCreating(ModelBuilder builder)
    {
        builder.Entity<Disciplina>()
            .HasKey(x => x.Id);

        builder.Entity<Disciplina>()
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();
    }
}
