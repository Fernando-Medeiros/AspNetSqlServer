namespace WebAPI.Endpoints.Alunos;

public static class RecuperarAluno
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("{id:guid}", async (
            Guid? id,
            DatabaseContext context) =>
        {
            var aluno = await context.Alunos
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (aluno == null)
                return Results.NotFound("Aluno não encontrado");

            return Results.Ok(aluno);

        }).Produces(200, typeof(Aluno));
    }
}
