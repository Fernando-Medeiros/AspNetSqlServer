namespace WebAPI.Endpoints.CursosRequest;

public record CadastroCursoRequest(
    string? Nome,
    Guid? UniversidadeId);
