namespace WebAPI.Endpoints.Disciplinas;

public static class CadastrarDisciplina
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("", async (
            [FromBody] DisciplinaRequest request,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            var disciplinaNome = await context.Disciplinas
                .Where(x => x.Nome == request.Nome)
                .Select(x => x.Nome)
                .FirstOrDefaultAsync(cancellationToken);

            if (disciplinaNome == request.Nome)
                return Results.BadRequest($"A disciplina {request.Nome} já està cadastrada!");

            Disciplina disciplina = new()
            {
                Id = new Guid(),
                Nome = request.Nome!,
            };

            context.Disciplinas.Add(disciplina);

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created();
        }).Produces(201);
    }
}
