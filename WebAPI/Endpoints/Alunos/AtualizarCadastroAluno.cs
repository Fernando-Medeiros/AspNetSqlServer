namespace WebAPI.Endpoints.Alunos;

public static class AtualizarCadastroAluno
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPatch("atualizar-cadastro/{id:guid}", async (
          Guid? id,
          [FromBody] string Nome,
          DatabaseContext context) =>
        {
            var aluno = await context.Alunos
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (aluno == null)
                return Results.NotFound("Aluno não encontrado");

            aluno.Nome = Nome;

            context.Alunos.Entry(aluno).State = EntityState.Modified;

            context.Alunos.Update(aluno);

            await context.SaveChangesAsync();

            return Results.NoContent();
        }).Produces(204);
    }
}
