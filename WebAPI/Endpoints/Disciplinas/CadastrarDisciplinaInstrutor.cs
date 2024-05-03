namespace WebAPI.Endpoints.Disciplinas;

public static class CadastrarDisciplinaInstrutor
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("{disciplinaId:guid}/{instrutorId:guid}", async (
            Guid disciplinaId,
            Guid instrutorId,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            var instrutor = await context.Instrutores
              .Where(x => x.Id == instrutorId)
              .FirstOrDefaultAsync(cancellationToken);

            if (instrutor == null)
                return Results.NotFound("Instrutor não encontrado");

            var disciplina = await context.Disciplinas
                .Include(x => x.Instrutores)
                .Where(x => x.Id == disciplinaId)
                .FirstOrDefaultAsync(cancellationToken);

            if (disciplina == null)
                return Results.NotFound("Disciplina não encontrada");

            if (disciplina.Instrutores.Contains(instrutor))
                return Results.BadRequest("Instrutor já cadastrada!");

            disciplina.Instrutores.Add(instrutor);

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created();
        }).Produces(201);
    }
}
