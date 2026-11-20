using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.Dtos.PrendasBase;
using Sora_AppProject.Application.UseCases.PrendasBase.Commands;
using Sora_AppProject.Application.UseCases.PrendasBase.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/prendas-base")]
public class PrendasBaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrendasBaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllPrendasBaseQuery(pageNumber, pageSize));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetPrendaBaseByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePrendaBaseRequestDto request)
    {
        var id = await _mediator.Send(new AddPrendaBaseCommand(request));
        return Ok(new { id, message = "Prenda base creada correctamente" });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePrendaBaseRequestDto request)
    {
        await _mediator.Send(new UpdatePrendaBaseCommand(id, request));
        return Ok(new { message = "Prenda base actualizada correctamente" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePrendaBaseCommand(id));
        return Ok(new { message = "Prenda base eliminada correctamente" });
    }
}
