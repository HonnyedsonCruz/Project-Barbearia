using Microsoft.EntityFrameworkCore;
using BarbeariaApi.Models;

namespace BarbeariaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Barbeiro> Barbeiros { get; set; }
    public DbSet<DiaTrabalho> DiasTrabalho { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
}