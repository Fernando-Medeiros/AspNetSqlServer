namespace WebAPI.Endpoints.Universidades;

public static class RecuperarUniversidade
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("{universidadeId:guid}", async (
            Guid universidadeId,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            UniversidadeCursosResponse? universidade = await context.Universidades
                .AsNoTracking()
                .Where(x => x.Id == universidadeId)
                .Select(x => new UniversidadeCursosResponse(
                    new Identificador(x.Id, x.Nome),
                    x.Cursos.Select(x => new Identificador(x.Id, x.Nome))
                    ))
                .FirstOrDefaultAsync(cancellationToken);

            return universidade is UniversidadeCursosResponse
                ? Results.Ok(universidade)
                : Results.NotFound("Universidade não encontrado");

        }).Produces(200, typeof(UniversidadeCursosResponse));


        route.MapGet("", async (
          DatabaseContext context,
          CancellationToken cancellationToken) =>
        {
            var universidades = await context.Universidades
                .AsNoTracking()
                .Select(x => new Identificador(x.Id, x.Nome))
                .ToListAsync(cancellationToken);

            return Results.Ok(universidades);

        }).Produces(200, typeof(IEnumerable<Identificador>));
    }
}
