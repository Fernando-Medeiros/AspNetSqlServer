namespace WebAPI.Endpoints.Alunos;

public static class AtualizarAluno
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPatch("{alunoId:guid}", async (
          Guid alunoId,
          [FromBody] string Nome,
          DatabaseContext context,
          CancellationToken cancellationToken) =>
        {
            var aluno = await context.Alunos
                .Where(x => x.Id == alunoId)
                .FirstOrDefaultAsync(cancellationToken);

            if (aluno == null)
                return Results.NotFound("Aluno não encontrado");

            aluno.Nome = Nome;

            context.Alunos.Update(aluno);

            await context.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        }).Produces(204);
    }
}
