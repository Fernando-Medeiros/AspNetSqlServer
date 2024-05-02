namespace WebAPI.Endpoints.Cursos;

public static class RecuperarCurso
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("{cursoId:guid}", async (
            Guid cursoId,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            var curso = await context.Cursos
                .AsNoTracking()
                .Where(x => x.Id == cursoId)
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.Disciplinas.Count,
                    Disciplinas = x.Disciplinas.Select(x => new { x.Id, x.Nome })
                })
                .FirstOrDefaultAsync(cancellationToken);

            return curso == null
                ? Results.NotFound("Curso não encontrado")
                : Results.Ok(curso);

        }).Produces(200, typeof(object));


        route.MapGet("", async (
           DatabaseContext context,
           CancellationToken cancellationToken) =>
        {
            var cursos = await context.Cursos
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Results.Ok(cursos);

        }).Produces(200, typeof(IEnumerable<Curso>));
    }
}
