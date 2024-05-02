namespace WebAPI.Endpoints.Universidades.Data;

public sealed record UniversidadeCursosResponse(
    Identificador Universidade,
    IEnumerable<Identificador> Cursos);
