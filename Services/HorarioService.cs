using BarbeariaApi.DTOs;
using BarbeariaApi.Repositories;

namespace BarbeariaApi.Services;

public class HorarioService
{
    private readonly AgendamentoRepository _agendamentoRepo;
    private readonly DiaTrabalhoRepository _diaTrabalhoRepo;

    private static readonly List<TimeOnly> HorariosFixos = new()
    {
        new TimeOnly(8, 0),  new TimeOnly(8, 30),
        new TimeOnly(9, 0),  new TimeOnly(9, 30),
        new TimeOnly(10, 0), new TimeOnly(10, 30),
        new TimeOnly(11, 0), new TimeOnly(11, 30),
        new TimeOnly(14, 0), new TimeOnly(14, 30),
        new TimeOnly(15, 0), new TimeOnly(15, 30),
        new TimeOnly(16, 0), new TimeOnly(16, 30),
        new TimeOnly(17, 0), new TimeOnly(17, 30)
    };

    public HorarioService(AgendamentoRepository agendamentoRepo, DiaTrabalhoRepository diaTrabalhoRepo)
    {
        _agendamentoRepo = agendamentoRepo;
        _diaTrabalhoRepo = diaTrabalhoRepo;
    }

    public async Task<List<HorarioDisponivelDTO>> ObterHorariosDisponiveis(DateOnly data)
    {
        var diaDisponivel = await _diaTrabalhoRepo.DiaDisponivel(data);
        if (!diaDisponivel) return new List<HorarioDisponivelDTO>();

        var resultado = new List<HorarioDisponivelDTO>();
        foreach (var horario in HorariosFixos)
        {
            var ocupado = await _agendamentoRepo.HorarioOcupado(data, horario);
            resultado.Add(new HorarioDisponivelDTO
            {
                Horario = horario,
                Disponivel = !ocupado
            });
        }
        return resultado;
    }
}