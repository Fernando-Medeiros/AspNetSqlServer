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
                .Include(x => x.Cursos)
                .Where(x => x.Id == request.UniversidadeId)
                .FirstOrDefaultAsync(cancellationToken);

            if (universidade == null)
                return Results.NotFound("Universidade não encontrada");

            if (universidade.Cursos.Any(x => x.Nome == request.Nome))
                return Results.NotFound(
                    $"Curso {request.Nome} já está cadastro na Universidade {universidade.Nome}");

            universidade.Cursos.Add(new()
            {
                Id = new Guid(),
                Nome = request.Nome,
                UniversidadeId = request.UniversidadeId,
            });

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created();
        }).Produces(201);
    }
}
