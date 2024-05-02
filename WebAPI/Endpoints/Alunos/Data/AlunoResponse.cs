namespace WebAPI.Endpoints.Alunos.Data;

public sealed record Identificador(Guid Id, string Nome);

public sealed record AlunoResponse(
    Identificador Aluno,
    Identificador Curso,
    Identificador Universidade,
    IEnumerable<Identificador> Disciplinas);

