using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence.Contexts;

public sealed class DatabaseContext(
    DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Instrutor> Instrutores { get; set; }
    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<Universidade> Universidades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Aluno.OnCreating(modelBuilder);
        Curso.OnCreating(modelBuilder);
        Instrutor.OnCreating(modelBuilder);
        Disciplina.OnCreating(modelBuilder);
        Universidade.OnCreating(modelBuilder);
    }
}
