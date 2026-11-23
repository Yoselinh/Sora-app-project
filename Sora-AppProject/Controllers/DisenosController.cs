using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Disenos.Commands;
using Sora_AppProject.Application.UseCases.Disenos.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DisenosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllDisenosQuery());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(
            new GetDisenoByIdQuery { Id = id });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddDisenoCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateDisenoCommand command)
    {
        command.Id = id;

        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(
            new DeleteDisenoCommand { Id = id });

        return NoContent();
    }
}