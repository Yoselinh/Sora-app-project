using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.Features.Cotizaciones.Queries;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CotizacionesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CotizacionesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Cotizacione>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllCotizacionesQuery());
        return Ok(result);
    }
}
