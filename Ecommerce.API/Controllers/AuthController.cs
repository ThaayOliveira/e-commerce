using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        await _service.Registrar(dto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        var token = await _service.Login(dto);
        return Ok(new { token });
    }
}