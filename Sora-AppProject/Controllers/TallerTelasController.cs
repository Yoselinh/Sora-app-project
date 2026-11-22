using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.TallerTelas.Commands;
using Sora_AppProject.Application.UseCases.TallerTelas.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TallerTelasController(IMediator mediator) : ControllerBase
{
    [HttpGet("taller/{tallerId:int}")]
    public async Task<IActionResult> GetByTaller(int tallerId)
    {
        var result = await mediator.Send(new GetTelasByTallerQuery { TallerId = tallerId });
        return Ok(result);
    }

    [HttpGet("{tallerId:int}/{telaId:int}")]
    public async Task<IActionResult> GetById(int tallerId, int telaId)
    {
        var result = await mediator.Send(new GetTallerTelaByIdQuery { TallerId = tallerId, TelaId = telaId });
        return Ok(result);
    }

    //commands

    [HttpPost]
    public async Task<IActionResult> Add(AddTallerTelaCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpPut("{tallerId:int}/{telaId:int}")]
    public async Task<IActionResult> Update(int tallerId, int telaId, UpdateTallerTelaCommand command)
    {
        command.TallerId = tallerId;
        command.TelaId = telaId;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{tallerId:int}/{telaId:int}")]
    public async Task<IActionResult> Delete(int tallerId, int telaId)
    {
        await mediator.Send(new DeleteTallerTelaCommand { TallerId = tallerId, TelaId = telaId });
        return NoContent();
    }
}