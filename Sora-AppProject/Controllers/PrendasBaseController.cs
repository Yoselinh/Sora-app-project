using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.Features.PrendasBase.Commands;
using Sora_AppProject.Application.Features.PrendasBase.Queries;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrendasBaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrendasBaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Sora_AppProject.Domain.Models.PrendasBase>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllPrendasBaseQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sora_AppProject.Domain.Models.PrendasBase>> GetById(int id)
    {
        var result = await _mediator.Send(new GetPrendaBaseByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreatePrendaBaseCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
}
