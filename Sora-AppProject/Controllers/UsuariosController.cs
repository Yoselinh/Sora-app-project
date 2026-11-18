using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Usuarios.Commands;
using Sora_AppProject.Application.UseCases.Usuarios.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "admin")]
public class UsuariosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllUsuariosQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetUsuarioByIdQuery { Id = id });
        return Ok(result);
    }
    
    //commands
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUsuarioCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteUsuarioCommand { Id = id });
        return NoContent();
    }
}
