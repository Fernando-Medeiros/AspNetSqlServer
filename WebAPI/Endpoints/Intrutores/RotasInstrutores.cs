namespace WebAPI.Endpoints.Instrutores;

internal static class RotasInstrutores
{
    public static void MapGroup(IEndpointRouteBuilder routeBuilder)
    {
        var route = routeBuilder
            .MapGroup("/api/v1/instrutor")
            .WithTags("Instrutor")
            .WithOpenApi();

        RecuperarInstrutor.Map(route);
        CadastrarInstrutor.Map(route);
    }
}
