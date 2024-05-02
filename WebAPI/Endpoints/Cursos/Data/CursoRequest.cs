namespace WebAPI.Endpoints.Cursos.Data;

public sealed record CursoRequest(
    string Nome,
    Guid UniversidadeId);
