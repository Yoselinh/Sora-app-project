using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Usuarios.Commands;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    //agregar usuario
    [HttpPost("register")]
    public async Task<IActionResult> Register(AddUsuarioCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { message = "Usuario registrado." });
       
    }
}