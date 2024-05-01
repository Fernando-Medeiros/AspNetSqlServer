namespace WebAPI.Endpoints.Universidades;

public static class RecuperarUniversidade
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("{id:guid}", async (
            Guid? id,
            DatabaseContext context) =>
        {
            var universidade = await context.Universidades
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (universidade == null)
                return Results.NotFound("Universidade não encontrado");

            return Results.Ok(universidade);

        }).Produces(200, typeof(Universidade));


        route.MapGet("", async (
          DatabaseContext context) =>
        {
            var universidades = await context.Universidades
                .AsNoTracking()
                .ToListAsync();

            return Results.Ok(universidades);

        }).Produces(200, typeof(List<Universidade>));
    }
}
