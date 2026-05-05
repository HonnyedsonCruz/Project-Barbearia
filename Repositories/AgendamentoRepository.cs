using Microsoft.EntityFrameworkCore;
using BarbeariaApi.Data;
using BarbeariaApi.Models;

namespace BarbeariaApi.Repositories;

public class AgendamentoRepository
{
    private readonly AppDbContext _context;

    public AgendamentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Agendamento> Criar(Agendamento agendamento)
    {
        _context.Agendamentos.Add(agendamento);
        await _context.SaveChangesAsync();
        return agendamento;
    }

    public async Task<bool> HorarioOcupado(DateOnly data, TimeOnly horario) =>
        await _context.Agendamentos.AnyAsync(a =>
            a.Data == data &&
            a.Horario == horario &&
            a.Status == StatusAgendamento.Confirmado);

    public async Task<List<Agendamento>> ListarPorData(DateOnly data) =>
        await _context.Agendamentos
            .Where(a => a.Data == data && a.Status == StatusAgendamento.Confirmado)
            .OrderBy(a => a.Horario)
            .ToListAsync();

    public async Task<List<Agendamento>> ListarPorMes(int ano, int mes) =>
        await _context.Agendamentos
            .Where(a => a.Data.Year == ano && a.Data.Month == mes)
            .OrderBy(a => a.Data).ThenBy(a => a.Horario)
            .ToListAsync();

    public async Task<Agendamento?> BuscarPorId(int id) =>
        await _context.Agendamentos.FindAsync(id);

    public async Task Salvar() =>
        await _context.SaveChangesAsync();
}