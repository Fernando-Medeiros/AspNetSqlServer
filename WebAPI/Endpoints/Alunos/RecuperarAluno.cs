using WebAPI.Endpoints.Alunos.Data;

namespace WebAPI.Endpoints.Alunos;

public static class RecuperarAluno
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapGet("{alunoId:guid}", async (
            Guid? alunoId,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            AlunoResponse? aluno = await context
                .Alunos
                .AsNoTracking()
                .Where(x => x.Id == alunoId)
                .Select(x => new AlunoResponse(
                    new Identificador(x.Id, x.Nome),
                    new Identificador(x.CursoId, x.Curso.Nome),
                    new Identificador(x.UniversidadeId, x.Universidade.Nome),
                    x.Curso.Disciplinas.Select(x => new Identificador(x.Id, x.Nome))))
                .FirstOrDefaultAsync(cancellationToken);

            return aluno is AlunoResponse
                ? Results.Ok(aluno)
                : Results.NotFound("Aluno não encontrado");

        }).Produces(200, typeof(AlunoResponse));
    }
}
