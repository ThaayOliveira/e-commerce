using Ecommerce.Infrastructure.Data;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task Registrar(RegisterDTO dto)
    {
        var existe = await _context.Usuarios
            .AnyAsync(u => u.Email == dto.Email);

        if (existe)
            throw new BadRequestException("Email já cadastrado");

        var senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

        var usuario = new Usuario(dto.Nome, dto.Email, senha);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<string> Login(LoginDTO dto)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (usuario == null)
            throw new UnauthorizedException("Credenciais inválidas");

        var senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha);

        if (!senhaValida)
            throw new UnauthorizedException("Credenciais inválidas");

        return GerarToken(usuario);
    }

    private string GerarToken(Usuario usuario)
    {
        var jwt = _config.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwt["Key"]!);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}