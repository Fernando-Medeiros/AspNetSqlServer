﻿using WebAPI.Endpoints.Alunos.Data;

namespace WebAPI.Endpoints.Alunos;

public static class CadastrarAluno
{
    public static void Map(IEndpointRouteBuilder route)
    {
        route.MapPost("", async (
            [FromBody] AlunoRequest request,
            DatabaseContext context,
            CancellationToken cancellationToken) =>
        {
            var universidade = await context.Universidades
                .AsNoTracking()
                .Where(x => x.Id == request.UniversidadeId)
                .FirstOrDefaultAsync(cancellationToken);

            if (universidade == null)
                return Results.NotFound("Universidade não encontrada");

            var curso = await context.Cursos
                .AsNoTracking()
                .Where(x => x.Id == request.CursoId)
                .Where(x => x.UniversidadeId == request.UniversidadeId)
                .FirstOrDefaultAsync(cancellationToken);

            if (curso == null)
                return Results.NotFound("Curso não encontrado");

            Aluno aluno = new()
            {
                Id = new Guid(),
                Nome = request.Nome!,
                CursoId = curso.Id,
                UniversidadeId = universidade.Id,
            };

            context.Alunos.Add(aluno);

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created();
        }).Produces(201);
    }
}
