namespace WebAPI.Endpoints.Cursos;

public static class CadastrarCurso
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("", async (
            [FromBody] CursoRequest request,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            var universidade = await context.Universidades
                .AsNoTracking()
                .Where(x => x.Id == request.UniversidadeId)
                .Include(x => x.Cursos)
                .FirstOrDefaultAsync(cancellationToken);

            if (universidade == null)
                return Results.NotFound("Universidade não encontrada");

            if (universidade.Cursos.Any(x => x.Nome == request.Nome))
                return Results.NotFound(
                    $"Curso {request.Nome} já está cadastro na Universidade {universidade.Nome}");

            Curso curso = new()
            {
                Id = new Guid(),
                Nome = request.Nome,
                UniversidadeId = request.UniversidadeId,
            };

            context.Cursos.Add(curso);

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created();
        }).Produces(201);
    }
}
