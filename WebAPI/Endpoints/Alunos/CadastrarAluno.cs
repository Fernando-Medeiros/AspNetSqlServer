namespace WebAPI.Endpoints.Alunos;

public static class CadastrarAluno
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("cadastro", async (
            [FromBody] CadastroAlunoRequest request,
            DatabaseContext context) =>
        {
            var universidade = await context.Universidades
                .AsNoTracking()
                .Where(x => x.Id == request.UniversidadeId)
                .FirstOrDefaultAsync();

            if (universidade == null)
                return Results.NotFound("Universidade não encontrada");

            var curso = await context.Cursos
                .AsNoTracking()
                .Where(x => x.Id == request.CursoId)
                .Where(x => x.UniversidadeId == request.UniversidadeId)
                .FirstOrDefaultAsync();

            if (curso == null)
                return Results.NotFound("Curso não encontrado");

            Aluno aluno = new()
            {
                Id = new Guid(),
                Nome = request.Nome!,
                CursoId = curso.Id,
                UniversidadeId = universidade.Id,
            };

            context.Alunos.Add(aluno);

            await context.SaveChangesAsync();

            return Results.Created("", aluno.Id);

        }).Produces(201, typeof(Aluno));
    }
}
