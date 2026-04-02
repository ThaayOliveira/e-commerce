using Ecommerce.Infrastructure.Data;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        AppDbContext context,
        IConfiguration config,
        ILogger<AuthService> logger)
    {
        _context = context;
        _config = config;
        _logger = logger;
    }

    public async Task Registrar(RegisterDTO dto)
    {
        _logger.LogInformation("Tentativa de registro para {Email}", dto.Email);

        var existe = await _context.Usuarios
            .AnyAsync(u => u.Email == dto.Email);

        if (existe)
        {
            _logger.LogWarning("Registro falhou - email já existe: {Email}", dto.Email);
            throw new BadRequestException("Email já cadastrado");
        }

        var senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

        var usuario = new Usuario(dto.Nome, dto.Email, senha)
        {
            Role = "User"
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Usuário registrado com sucesso: {Email}", dto.Email);
    }

    public async Task<string> Login(LoginDTO dto)
    {
        _logger.LogInformation("Tentativa de login para {Email}", dto.Email);

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha))
        {
            _logger.LogWarning("Login falhou para {Email}", dto.Email);
            throw new UnauthorizedException("Credenciais inválidas");
        }

        _logger.LogInformation("Login realizado com sucesso para {Email}", dto.Email);

        return GerarToken(usuario);
    }

    private string GerarToken(Usuario usuario)
    {
        var jwt = _config.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwt["Key"]!);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Role ?? "User"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}