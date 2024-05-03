namespace WebAPI.Endpoints.Instrutores.Data;

public sealed record InstrutorResponse(
    Identificador Instrutor,
    Identificador Universidade,
    IEnumerable<Identificador> Disciplinas);

