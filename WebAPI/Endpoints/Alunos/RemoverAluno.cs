namespace WebAPI.Endpoints.Alunos;

public static class RemoverAluno
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapDelete("{alunoId:guid}", async (
           Guid alunoId,
           DatabaseContext context,
           CancellationToken cancellationToken) =>
        {
            var aluno = await context.Alunos
                .Where(x => x.Id == alunoId)
                .FirstOrDefaultAsync(cancellationToken);

            if (aluno == null)
                return Results.NotFound("Aluno não encontrado");

            context.Alunos.Remove(aluno);

            await context.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        }).Produces(204);
    }
}
