namespace WebAPI.Endpoints.Disciplinas;

internal static class RotasDisciplinas
{
    public static void MapGroup(IEndpointRouteBuilder routeBuilder)
    {
        var route = routeBuilder
            .MapGroup("/api/v1/disciplina")
            .WithTags("Disciplina")
            .WithOpenApi();

        RecuperarDisciplina.Map(route);
        CadastrarDisciplina.Map(route);
        CadastrarDisciplinaInstrutor.Map(route);
    }
}
