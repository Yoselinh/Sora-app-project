using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.Dtos.Telas;
using Sora_AppProject.Application.UseCases.Telas.Commands;
using Sora_AppProject.Application.UseCases.Telas.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/telas")]
[Authorize]
public class TelasController : ControllerBase
{
    private readonly IMediator _mediator;

    public TelasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllTelasQuery(pageNumber, pageSize));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetTelaByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Add([FromBody] CreateTelaRequestDto request)
    {
        var id = await _mediator.Send(new AddTelaCommand(request));
        return Ok(new { id, message = "Tela creada correctamente" });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTelaRequestDto request)
    {
        await _mediator.Send(new UpdateTelaCommand(id, request));
        return Ok(new { message = "Tela actualizada correctamente" });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteTelaCommand(id));
        return Ok(new { message = "Tela eliminada correctamente" });
    }
}
