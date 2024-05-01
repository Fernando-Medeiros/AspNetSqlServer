namespace WebAPI.Endpoints.Cursos;

internal static class RotasCursos
{
    public static void MapGroup(IEndpointRouteBuilder routeBuilder)
    {
        var route = routeBuilder
            .MapGroup("/api/v1/curso")
            .WithTags("Cursos")
            .WithOpenApi();

        RecuperarCursos.Map(route);
        CadastrarCursos.Map(route);
    }
}
