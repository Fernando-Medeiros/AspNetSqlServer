namespace WebAPI.Endpoints.Alunos;

internal static class RotasAlunos
{
    public static void MapGroup(IEndpointRouteBuilder routeBuilder)
    {
        var route = routeBuilder
            .MapGroup("/api/v1/aluno")
            .WithTags("Aluno")
            .WithOpenApi();

        RecuperarAluno.Map(route);
        CadastrarAluno.Map(route);
    }
}
