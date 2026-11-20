using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.DisenoTelas.Commands;
using Sora_AppProject.Application.UseCases.DisenoTelas.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/diseno-telas")]
public class DisenoTelasController : ControllerBase
{
    private readonly IMediator _mediator;

    public DisenoTelasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllDisenoTelasQuery()
        );

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetDisenoTelaByIdQuery(id)
        );

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] AddDisenoTelaCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(new
        {
            id,
            message = "DiseñoTela agregado correctamente"
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateDisenoTelaCommand command)
    {
        var request = command with
        {
            Id = id
        };

        await _mediator.Send(request);

        return Ok(new
        {
            message = "DiseñoTela actualizado correctamente"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(
            new DeleteDisenoTelaCommand(id)
        );

        return Ok(new
        {
            message = "DiseñoTela eliminado correctamente"
        });
    }
}