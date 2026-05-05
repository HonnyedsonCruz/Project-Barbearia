namespace BarbeariaApi.Models;

public class DiaTrabalho
{
    public int Id { get; set; }
    public DateOnly Data { get; set; }
    public bool Ativo { get; set; } = true;
}