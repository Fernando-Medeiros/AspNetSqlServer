using Microsoft.EntityFrameworkCore;

namespace Persistence.Models;

// Dependente
public class Aluno
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Requer à chave estrangeira
    public Guid CursoId { get; set; }
    public Guid UniversidadeId { get; set; }

    // Requer a referêcia para navegar até a principal
    public Curso Curso { get; set; } = null!;
    public Universidade Universidade { get; set; } = null!;

    internal static void OnCreating(ModelBuilder builder)
    {
        builder.Entity<Aluno>()
            .HasKey(x => x.Id);

        builder.Entity<Aluno>()
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();
    }
}
