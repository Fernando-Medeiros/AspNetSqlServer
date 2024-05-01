namespace WebAPI.Endpoints.Universidades;

public static class CadastrarUniversidade
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("cadastro", async (
            [FromBody] CadastroUniversidadeRequest request,
            DatabaseContext context) =>
        {
            Universidade universidade = new()
            {
                Id = new Guid(),
                Nome = request.Nome!,
            };

            context.Universidades.Add(universidade);

            await context.SaveChangesAsync();

            return Results.Created("", universidade.Id);

        }).Produces(201, typeof(Universidade));
    }
}
