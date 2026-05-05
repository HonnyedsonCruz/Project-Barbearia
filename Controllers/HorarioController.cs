using Microsoft.AspNetCore.Mvc;
using BarbeariaApi.Services;

namespace BarbeariaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HorarioController : ControllerBase
{
    private readonly HorarioService _service;

    public HorarioController(HorarioService service)
    {
        _service = service;
    }

    [HttpGet("{data}")]
    public async Task<IActionResult> ObterHorarios(DateOnly data)
    {
        var horarios = await _service.ObterHorariosDisponiveis(data);
        return Ok(horarios);
    }
}