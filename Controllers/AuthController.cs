using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BarbeariaApi.Data;
using BarbeariaApi.Models;

namespace BarbeariaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var barbeiro = await _context.Barbeiros
            .FirstOrDefaultAsync(b => b.Email == dto.Email);

        if (barbeiro == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, barbeiro.SenhaHash))
            return Unauthorized(new { message = "Email ou senha inválidos." });

        var token = GerarToken(barbeiro);
        return Ok(new { token, nome = barbeiro.Nome });
    }

    [HttpPost("setup")]
    public async Task<IActionResult> Setup([FromBody] SetupDTO dto)
    {
        if (await _context.Barbeiros.AnyAsync())
            return BadRequest(new { message = "Barbeiro já cadastrado." });

        var barbeiro = new Barbeiro
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
        };

        _context.Barbeiros.Add(barbeiro);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Barbeiro criado com sucesso!" });
    }

    private string GerarToken(Barbeiro barbeiro)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, barbeiro.Id.ToString()),
            new Claim(ClaimTypes.Name, barbeiro.Nome),
            new Claim(ClaimTypes.Email, barbeiro.Email)
        };
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginDTO
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}

public class SetupDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}