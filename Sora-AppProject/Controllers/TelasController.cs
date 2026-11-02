using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.Features.Telas.Queries;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelasController : ControllerBase
{
    private readonly IMediator _mediator;

    public TelasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Tela>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllTelasQuery());
        return Ok(result);
    }
}
