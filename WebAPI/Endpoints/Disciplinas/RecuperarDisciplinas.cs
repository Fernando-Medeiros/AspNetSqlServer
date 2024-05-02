namespace WebAPI.Endpoints.Disciplinas;

public static class RecuperarDisciplinas
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("{id:guid}", async (
            Guid? id,
            DatabaseContext context) =>
        {
            var disciplina = await context.Disciplinas
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (disciplina == null)
                return Results.NotFound("Disciplina não encontrada");

            return Results.Ok(disciplina);

        }).Produces(200, typeof(Disciplina));


        route.MapGet("", async (
           DatabaseContext context) =>
        {
            var disciplinas = await context.Disciplinas
                .AsNoTracking()
                .ToListAsync();

            return Results.Ok(disciplinas);

        }).Produces(200, typeof(List<Disciplina>));
    }
}
