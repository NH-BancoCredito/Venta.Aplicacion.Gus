using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ventas.Application.CasosUso.AdministrarVentas.RegistrarVenta;

namespace Ventas.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VentaController : ControllerBase
{
    private readonly IMediator _mediator;
    public VentaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistrarVentaRequest request)
    {
        var response = await _mediator.Send(request);

        return Ok(response);
    }
}