namespace WebAPI.Endpoints.Cursos.Data;

public sealed record CursoDisciplinasResponse(
    Identificador Curso,
    IEnumerable<Identificador> Disciplinas);