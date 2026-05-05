namespace BarbeariaApi.DTOs;

public class CriarAgendamentoDTO
{
    public string NomeCliente { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public DateOnly Data { get; set; }
    public TimeOnly Horario { get; set; }
}

public class AgendamentoResponseDTO
{
    public int Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public DateOnly Data { get; set; }
    public TimeOnly Horario { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class HorarioDisponivelDTO
{
    public TimeOnly Horario { get; set; }
    public bool Disponivel { get; set; }
}