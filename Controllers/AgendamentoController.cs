using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BarbeariaApi.DTOs;
using BarbeariaApi.Services;

namespace BarbeariaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendamentoController : ControllerBase
{
    private readonly AgendamentoService _service;

    public AgendamentoController(AgendamentoService service)
    {
        _service = service;
    }

    // Público — cliente agenda sem login
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarAgendamentoDTO dto)
    {
        var (agendamento, erro) = await _service.Criar(dto);
        if (erro != null) return BadRequest(new { message = erro });
        return CreatedAtAction(nameof(Criar), agendamento);
    }

    // Protegido — só o barbeiro vê
    [Authorize]
    [HttpGet("data/{data}")]
    public async Task<IActionResult> ListarPorData(DateOnly data)
    {
        var agendamentos = await _service.ListarPorData(data);
        return Ok(agendamentos);
    }

    [Authorize]
    [HttpGet("mes/{ano}/{mes}")]
    public async Task<IActionResult> ListarPorMes(int ano, int mes)
    {
        var agendamentos = await _service.ListarPorMes(ano, mes);
        return Ok(agendamentos);
    }

    [Authorize]
    [HttpPatch("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var (sucesso, erro) = await _service.Cancelar(id);
        if (!sucesso) return NotFound(new { message = erro });
        return NoContent();
    }
}