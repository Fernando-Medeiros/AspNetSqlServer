﻿namespace WebAPI.Endpoints.Universidades;

public static class CadastrarUniversidade
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("", async (
            [FromBody] UniversidadeRequest request,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            var universidadeName = await context.Universidades
                .AsNoTracking()
                .Where(x => x.Nome == request.Nome)
                .Select(x => x.Nome)
                .FirstOrDefaultAsync(cancellationToken);

            if (universidadeName == request.Nome)
                return Results.BadRequest($"A Universidade {request.Nome} já está cadastrada!");

            Universidade universidade = new()
            {
                Id = new Guid(),
                Nome = request.Nome,
            };

            context.Universidades.Add(universidade);

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created();

        }).Produces(201);
    }
}
