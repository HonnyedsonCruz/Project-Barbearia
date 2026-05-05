using BarbeariaApi.DTOs;
using BarbeariaApi.Models;
using BarbeariaApi.Repositories;

namespace BarbeariaApi.Services;

public class AgendamentoService
{
    private readonly AgendamentoRepository _repository;
    private readonly DiaTrabalhoRepository _diaTrabalhoRepo;

    public AgendamentoService(AgendamentoRepository repository, DiaTrabalhoRepository diaTrabalhoRepo)
    {
        _repository = repository;
        _diaTrabalhoRepo = diaTrabalhoRepo;
    }

    public async Task<(AgendamentoResponseDTO? agendamento, string? erro)> Criar(CriarAgendamentoDTO dto)
    {
        var diaDisponivel = await _diaTrabalhoRepo.DiaDisponivel(dto.Data);
        if (!diaDisponivel)
            return (null, "Este dia não está disponível para agendamento.");

        var horarioOcupado = await _repository.HorarioOcupado(dto.Data, dto.Horario);
        if (horarioOcupado)
            return (null, "Este horário já está ocupado.");

        var agendamento = new Agendamento
        {
            NomeCliente = dto.NomeCliente,
            Telefone = dto.Telefone,
            Data = dto.Data,
            Horario = dto.Horario
        };

        var criado = await _repository.Criar(agendamento);
        return (MapearResponse(criado), null);
    }

    public async Task<List<AgendamentoResponseDTO>> ListarPorData(DateOnly data)
    {
        var agendamentos = await _repository.ListarPorData(data);
        return agendamentos.Select(MapearResponse).ToList();
    }

    public async Task<List<AgendamentoResponseDTO>> ListarPorMes(int ano, int mes)
    {
        var agendamentos = await _repository.ListarPorMes(ano, mes);
        return agendamentos.Select(MapearResponse).ToList();
    }

    public async Task<(bool sucesso, string? erro)> Cancelar(int id)
    {
        var agendamento = await _repository.BuscarPorId(id);
        if (agendamento == null) return (false, "Agendamento não encontrado.");
        agendamento.Status = StatusAgendamento.Cancelado;
        await _repository.Salvar();
        return (true, null);
    }

    private AgendamentoResponseDTO MapearResponse(Agendamento a) => new()
    {
        Id = a.Id,
        NomeCliente = a.NomeCliente,
        Telefone = a.Telefone,
        Data = a.Data,
        Horario = a.Horario,
        Status = a.Status.ToString()
    };
}