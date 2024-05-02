namespace WebAPI.Endpoints.Disciplinas;

public static class RecuperarDisciplina
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("{disciplinaId:guid}", async (
            Guid disciplinaId,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            DisciplinaInstrutoresResponse? disciplina = await context.Disciplinas
                .AsNoTracking()
                .Where(x => x.Id == disciplinaId)
                .Select(x => new DisciplinaInstrutoresResponse(
                    new Identificador(x.Id, x.Nome),
                    x.Instrutores.Select(x => new Identificador(x.Id, x.Nome))))
                .FirstOrDefaultAsync(cancellationToken);

            return disciplina is DisciplinaInstrutoresResponse
                ? Results.Ok(disciplina)
                : Results.NotFound("Disciplina não encontrada");

        }).Produces(200, typeof(DisciplinaInstrutoresResponse));


        route.MapGet("", async (
           DatabaseContext context,
           CancellationToken cancellationToken) =>
        {
            var disciplinas = await context.Disciplinas
                .AsNoTracking()
                .Select(x => new Identificador(x.Id, x.Nome))
                .ToListAsync(cancellationToken);

            return Results.Ok(disciplinas);

        }).Produces(200, typeof(IEnumerable<Identificador>));
    }
}
