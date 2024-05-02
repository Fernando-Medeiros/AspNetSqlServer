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
            CursoDisciplinasResponse? curso = await context.Cursos
                .AsNoTracking()
                .Where(x => x.Id == cursoId)
                .Select(x => new CursoDisciplinasResponse(
                    new Identificador(x.Id, x.Nome),
                    x.Disciplinas.Select(x => new Identificador(x.Id, x.Nome))))
                .FirstOrDefaultAsync(cancellationToken);

            return curso is CursoDisciplinasResponse
                ? Results.Ok(curso)
                : Results.NotFound("Curso não encontrado");

        }).Produces(200, typeof(CursoDisciplinasResponse));


        route.MapGet("", async (
           DatabaseContext context,
           CancellationToken cancellationToken) =>
        {
            var cursos = await context.Cursos
                .AsNoTracking()
                .Select(x => new Identificador(x.Id, x.Nome))
                .ToListAsync(cancellationToken);

            return Results.Ok(cursos);

        }).Produces(200, typeof(IEnumerable<Identificador>));
    }
}
