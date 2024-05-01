using Microsoft.EntityFrameworkCore;

namespace Persistence.Models;

// Dependente
public class Instrutor
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Requer à chave estrangeira
    public Guid UniversidadeId { get; set; }

    // Requer a referêcia para navegar até a principal
    public Universidade Universidade { get; set; } = null!;

    // Muitos para muitos básico  (N .. N)
    public ICollection<Disciplina> Disciplinas { get; } = [];

    internal static void OnCreating(ModelBuilder builder)
    {
        builder.Entity<Instrutor>()
            .HasKey(x => x.Id);

        builder.Entity<Instrutor>()
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();
    }
}
