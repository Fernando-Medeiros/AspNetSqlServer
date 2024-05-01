namespace WebAPI.Endpoints.Cursos;

public static class CadastrarCursos
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("cadastro", async (
            [FromBody] CadastroCursoRequest request,
            DatabaseContext context) =>
        {
            var universidade = await context.Universidades
                .AsNoTracking()
                .Where(x => x.Id == request.UniversidadeId)
                .FirstOrDefaultAsync();

            if (universidade == null)
                return Results.NotFound("Universidade não encontrada");

            Curso curso = new()
            {
                Id = new Guid(),
                Nome = request.Nome!,
                UniversidadeId = universidade.Id,
            };

            context.Cursos.Add(curso);

            await context.SaveChangesAsync();

            return Results.Created("", curso.Id);

        }).Produces(201, typeof(Curso));
    }
}
