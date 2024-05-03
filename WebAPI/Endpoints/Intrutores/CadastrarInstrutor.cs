namespace WebAPI.Endpoints.Instrutores;

public static class CadastrarInstrutor
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("", async (
            [FromBody] InstrutorRequest request,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            var universidade = await context.Universidades
                .AsNoTrackingWithIdentityResolution()
                .Where(x => x.Id == request.UniversidadeId)
                .Select(x => new
                {
                    Instrutor = x.Instrutores
                        .Where(x => x.Nome == request.Nome)
                        .Select(x => x.Nome)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (universidade == null)
                return Results.NotFound("Universidade não encontrada");

            if (universidade.Instrutor != null)
                return Results.NotFound("Instrutor já cadastrado!");

            context.Instrutores.Add(new()
            {
                Id = new Guid(),
                Nome = request.Nome,
                UniversidadeId = request.UniversidadeId,
            });

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created();
        }).Produces(201);
    }
}
