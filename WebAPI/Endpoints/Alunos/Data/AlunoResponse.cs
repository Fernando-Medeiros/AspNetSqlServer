namespace WebAPI.Endpoints.Alunos.Data;

public sealed record AlunoResponse(
    Identificador Aluno,
    Identificador Curso,
    Identificador Universidade,
    IEnumerable<Identificador> Disciplinas);

