using MediatR;

namespace Ventas.Application.CasosUso.AdministrarVentas.RegistrarVenta;

public class RegistrarVentaRequest : IRequest<RegistrarVentaResponse>
{

    public int IdCliente {  get; set; }

    public IEnumerable<RegistrarVentaDetalleRequest> Productos { get; set; }

}

public class RegistrarVentaDetalleRequest
{
    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public int Precio { get; set; }

}