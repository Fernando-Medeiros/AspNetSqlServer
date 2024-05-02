namespace WebAPI.Endpoints.Cursos;

public static class CadastrarCursoDisciplina
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("{cursoId:guid}/{disciplinaId:guid}", async (
            Guid cursoId,
            Guid disciplinaId,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            var disciplina = await context.Disciplinas
                .Where(x => x.Id == disciplinaId)
                .FirstOrDefaultAsync(cancellationToken);

            if (disciplina == null)
                return Results.NotFound("Disciplina não encontrada");

            var curso = await context.Cursos
                .Where(x => x.Id == cursoId)
                .Include(x => x.Disciplinas)
                .FirstOrDefaultAsync(cancellationToken);

            if (curso == null)
                return Results.NotFound("Curso não encontrado");

            if (curso.Disciplinas.Contains(disciplina))
                return Results.BadRequest("Disciplina já cadastrada!");

            curso.Disciplinas.Add(disciplina);

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created();
        }).Produces(201);
    }
}
