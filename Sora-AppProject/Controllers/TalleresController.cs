using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Talleres.Commands;
using Sora_AppProject.Application.UseCases.Talleres.Queries;
 
namespace Sora_AppProject.Controllers;
 
[ApiController]
[Route("api/talleres")]
public class TalleresController : ControllerBase
{
    private readonly IMediator _mediator;
 
    public TalleresController(IMediator mediator)
    {
        _mediator = mediator;
    }
 
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllTalleresQuery()
        );
 
        return Ok(result);
    }
 
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetTallerByIdQuery { Id = id }
        );
 
        if (result is null)
            return NotFound();
 
        return Ok(result);
    }
 
    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] AddTallerCommand command)
    {
        var id = await _mediator.Send(command);
 
        return Ok(new
        {
            id,
            message = "Taller creado correctamente"
        });
    }
 
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateTallerCommand command)
    {
        command.Id = id;
 
        await _mediator.Send(command);
 
        return Ok(new
        {
            message = "Taller actualizado correctamente"
        });
    }
 
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(
            new DeleteTallerCommand { Id = id }
        );
 
        return Ok(new
        {
            message = "Taller eliminado correctamente"
        });
    }
}