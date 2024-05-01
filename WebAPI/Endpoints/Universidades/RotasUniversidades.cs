namespace WebAPI.Endpoints.Universidades;

internal static class RotasUniversidades
{
    public static void MapGroup(IEndpointRouteBuilder routeBuilder)
    {
        var route = routeBuilder
            .MapGroup("/api/v1/universidade")
            .WithTags("Universidade")
            .WithOpenApi();

        RecuperarUniversidade.Map(route);
        CadastrarUniversidade.Map(route);
    }
}
