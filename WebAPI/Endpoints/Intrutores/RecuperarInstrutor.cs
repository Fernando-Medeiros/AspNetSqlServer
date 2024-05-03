namespace WebAPI.Endpoints.Instrutores;

public static class RecuperarInstrutor
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("{intstrutorId:guid}", async (
            Guid intstrutorId,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            InstrutorResponse? instrutor = await context
                .Instrutores
                .AsNoTracking()
                .Where(x => x.Id == intstrutorId)
                .Select(x => new InstrutorResponse(
                    new Identificador(x.Id, x.Nome),
                    new Identificador(x.UniversidadeId, x.Universidade.Nome),
                    x.Disciplinas.Select(x => new Identificador(x.Id, x.Nome))))
                .FirstOrDefaultAsync(cancellationToken);

            return instrutor is InstrutorResponse
                ? Results.Ok(instrutor)
                : Results.NotFound("Instrutor não encontrado");

        }).Produces(200, typeof(InstrutorResponse));
    }
}
