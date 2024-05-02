namespace WebAPI.Endpoints.Alunos.Data;

public sealed record AlunoRequest(
    string Nome,
    Guid CursoId,
    Guid UniversidadeId);
