namespace WebAPI.Endpoints.Disciplinas.Data;

public sealed record DisciplinaInstrutoresResponse(
    Identificador Disciplina,
    IEnumerable<Identificador> Instrutores);