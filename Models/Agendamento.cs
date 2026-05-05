namespace BarbeariaApi.Models;

public class Agendamento
{
    public int Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public DateOnly Data { get; set; }
    public TimeOnly Horario { get; set; }
    public StatusAgendamento Status { get; set; } = StatusAgendamento.Confirmado;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}

public enum StatusAgendamento
{
    Confirmado,
    Cancelado
}