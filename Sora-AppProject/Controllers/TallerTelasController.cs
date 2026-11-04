using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.TallerTelas.Commands;
using Sora_AppProject.Application.UseCases.TallerTelas.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/taller-telas")]
public class TallerTelasController : ControllerBase
{
    private readonly IMediator _mediator;

    public TallerTelasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("taller/{tallerId:int}")]
    public async Task<IActionResult> GetByTaller(int tallerId)
    {
        var result = await _mediator.Send(
            new GetTelasByTallerQuery { TallerId = tallerId }
        );

        return Ok(result);
    }

    [HttpGet("{tallerId:int}/{telaId:int}")]
    public async Task<IActionResult> GetById(int tallerId, int telaId)
    {
        var result = await _mediator.Send(
            new GetTallerTelaByIdQuery { TallerId = tallerId, TelaId = telaId }
        );

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] AddTallerTelaCommand command)
    {
        await _mediator.Send(command);

        return Ok(new
        {
            message = "Tela agregada al taller correctamente"
        });
    }

    [HttpPut("{tallerId:int}/{telaId:int}")]
    public async Task<IActionResult> Update(
        int tallerId,
        int telaId,
        [FromBody] UpdateTallerTelaCommand command)
    {
        command.TallerId = tallerId;
        command.TelaId = telaId;

        await _mediator.Send(command);

        return Ok(new
        {
            message = "Precio actualizado correctamente"
        });
    }

    [HttpDelete("{tallerId:int}/{telaId:int}")]
    public async Task<IActionResult> Delete(int tallerId, int telaId)
    {
        await _mediator.Send(
            new DeleteTallerTelaCommand { TallerId = tallerId, TelaId = telaId }
        );

        return Ok(new
        {
            message = "Tela eliminada del taller correctamente"
        });
    }
}