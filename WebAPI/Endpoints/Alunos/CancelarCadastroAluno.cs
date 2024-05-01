namespace WebAPI.Endpoints.Alunos;

public static class CancelarCadastroAluno
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapDelete("cancelar-cadastro/{id:guid}", async (
           Guid? id,
           DatabaseContext context
           ) =>
        {
            var aluno = await context.Alunos
                 .AsNoTracking()
                 .Where(x => x.Id == id)
                 .FirstOrDefaultAsync();

            if (aluno == null)
                return Results.NotFound("Aluno não encontrado");

            context.Alunos.Remove(aluno);

            await context.SaveChangesAsync();

            return Results.NoContent();
        }).Produces(204);
    }
}
