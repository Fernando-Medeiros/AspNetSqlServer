namespace WebAPI.Endpoints.AlunosRequest;

public record CadastroAlunoRequest(
    string? Nome,
    Guid? CursoId,
    Guid? UniversidadeId);
