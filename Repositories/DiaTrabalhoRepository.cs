using Microsoft.EntityFrameworkCore;
using BarbeariaApi.Data;
using BarbeariaApi.Models;

namespace BarbeariaApi.Repositories;

public class DiaTrabalhoRepository
{
    private readonly AppDbContext _context;

    public DiaTrabalhoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DiaDisponivel(DateOnly data) =>
        await _context.DiasTrabalho.AnyAsync(d => d.Data == data && d.Ativo);

    public async Task<List<DiaTrabalho>> ListarPorMes(int ano, int mes) =>
        await _context.DiasTrabalho
            .Where(d => d.Data.Year == ano && d.Data.Month == mes && d.Ativo)
            .OrderBy(d => d.Data)
            .ToListAsync();

    public async Task<DiaTrabalho> Adicionar(DiaTrabalho dia)
    {
        _context.DiasTrabalho.Add(dia);
        await _context.SaveChangesAsync();
        return dia;
    }

    public async Task<DiaTrabalho?> BuscarPorData(DateOnly data) =>
        await _context.DiasTrabalho.FirstOrDefaultAsync(d => d.Data == data);

    public async Task Salvar() =>
        await _context.SaveChangesAsync();
}