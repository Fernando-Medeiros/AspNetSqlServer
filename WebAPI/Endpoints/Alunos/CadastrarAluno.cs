namespace WebAPI.Endpoints.Alunos;

public static class CadastrarAluno
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("", async (
            [FromBody] AlunoRequest request,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            bool hasCurso = await context.Cursos
                .AsNoTracking()
                .Where(x => x.Id == request.CursoId)
                .Where(x => x.UniversidadeId == request.UniversidadeId)
                .Select(x => x is Curso)
                .FirstOrDefaultAsync(cancellationToken);

            if (hasCurso is false)
                return Results.NotFound("Curso não encontrado");

            context.Alunos.Add(new()
            {
                Id = new Guid(),
                Nome = request.Nome,
                CursoId = request.CursoId,
                UniversidadeId = request.UniversidadeId,
            });

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created();
        }).Produces(201);
    }
}
