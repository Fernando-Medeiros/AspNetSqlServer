namespace WebAPI.Endpoints.Instrutores.Data;

public sealed record InstrutorRequest(
    string Nome,
    Guid UniversidadeId);
