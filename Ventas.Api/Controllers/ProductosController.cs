using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ventas.Application.CasosUso.AdministrarProductos.ConsultarProductos;

namespace Ventas.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("consultar")]
    public async Task<IActionResult> Consultar([FromQuery] ConsultarProductosRequest request)
    {
        var response = await _mediator.Send(request);

        return Ok(response);
    }
}