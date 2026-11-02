using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Disenos.Commands;
using Sora_AppProject.Application.UseCases.Disenos.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/disenos")]
public class DisenosController : ControllerBase
{
    private readonly IMediator _mediator;

    public DisenosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllDisenosQuery()
        );

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetDisenoByIdQuery(id)
        );

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateDisenoCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(new
        {
            id,
            message = "Diseño creado correctamente"
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateDisenoCommand command)
    {
        var request = command with
        {
            Id = id
        };

        await _mediator.Send(request);

        return Ok(new
        {
            message = "Diseño actualizado correctamente"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(
            new DeleteDisenoCommand(id)
        );

        return Ok(new
        {
            message = "Diseño eliminado correctamente"
        });
    }
}