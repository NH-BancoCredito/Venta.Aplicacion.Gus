using Venta.Domain.Models;
using Venta.Domain.Repositories;

namespace Venta.Infrastructure.Repositories;

public class ProductoRepository : IProductoRepository
{
    public Task<bool> Adicionar(Producto entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Modificar(Producto entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(Producto entity)
    {
        throw new NotImplementedException();
    }

    public Task<Producto> ConsultarPorId(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Producto>> Consultar(string nombre)
    {
        throw new NotImplementedException();
    }

    public Task ReservarStock(int detalleIdProducto, int detalleCantidad)
    {
        throw new NotImplementedException();
    }
}