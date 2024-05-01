namespace WebAPI.Endpoints.Cursos;

public static class RecuperarCursos
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("{id:guid}", async (
            Guid? id,
            DatabaseContext context) =>
        {
            var curso = await context.Cursos
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (curso == null)
                return Results.NotFound("Curso não encontrado");

            return Results.Ok(curso);

        }).Produces(200, typeof(Curso));


        route.MapGet("", async (
           DatabaseContext context) =>
        {
            var cursos = await context.Cursos
                .AsNoTracking()
                .ToListAsync();

            return Results.Ok(cursos);

        }).Produces(200, typeof(List<Curso>));
    }
}
