using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BarbeariaApi.Data;
using BarbeariaApi.Models;
using BarbeariaApi.Repositories;

namespace BarbeariaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarbeiroController : ControllerBase
{
    private readonly DiaTrabalhoRepository _diaTrabalhoRepo;

    public BarbeiroController(DiaTrabalhoRepository diaTrabalhoRepo)
    {
        _diaTrabalhoRepo = diaTrabalhoRepo;
    }

    // Público — cliente consulta dias disponíveis
    [HttpGet("dias-disponiveis/{ano}/{mes}")]
    public async Task<IActionResult> DiasDisponiveis(int ano, int mes)
    {
        var dias = await _diaTrabalhoRepo.ListarPorMes(ano, mes);
        return Ok(dias.Select(d => d.Data));
    }

    // Protegido — só o barbeiro gerencia
    [Authorize]
    [HttpPost("dias-trabalho")]
    public async Task<IActionResult> AdicionarDia([FromBody] DateOnly data)
    {
        var existente = await _diaTrabalhoRepo.BuscarPorData(data);
        if (existente != null)
        {
            existente.Ativo = !existente.Ativo;
            await _diaTrabalhoRepo.Salvar();
            return Ok(new { message = existente.Ativo ? "Dia ativado." : "Dia desativado." });
        }

        await _diaTrabalhoRepo.Adicionar(new DiaTrabalho { Data = data });
        return Ok(new { message = "Dia de trabalho adicionado!" });
    }
}