using Microsoft.EntityFrameworkCore;

namespace Persistence.Models;

// Principal
public class Curso
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Requer à chave estrangeira
    public Guid UniversidadeId { get; set; }

    // Requer a referêcia para navegar até a principal
    public Universidade Universidade { get; set; } = null!;

    // Muitos para muitos básico  (N .. N)
    public ICollection<Aluno> Alunos { get; } = [];
    public ICollection<Disciplina> Disciplinas { get; } = [];

    internal static void OnCreating(ModelBuilder builder)
    {
        builder.Entity<Curso>()
            .HasKey(x => x.Id);

        builder.Entity<Curso>()
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<Curso>()
            .HasMany(x => x.Alunos)
            .WithOne(x => x.Curso)
            .HasForeignKey(x => x.CursoId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
