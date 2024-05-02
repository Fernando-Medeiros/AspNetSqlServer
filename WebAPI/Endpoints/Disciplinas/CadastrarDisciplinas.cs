namespace WebAPI.Endpoints.Disciplinas;

public static class CadastrarDisciplinas
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("cadastro", async (
            [FromBody] CadastroDisciplinaRequest request,
            DatabaseContext context) =>
        {
            Disciplina disciplina = new()
            {
                Id = new Guid(),
                Nome = request.Nome!,
            };

            context.Disciplinas.Add(disciplina);

            await context.SaveChangesAsync();

            return Results.Created("", disciplina.Id);

        }).Produces(201, typeof(Disciplina));
    }
}
