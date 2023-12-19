using AutoMapper;
using Venta.Domain.Repositories;
 

 
using Models = Venta.Domain.Models;


namespace Ventas.Application.CasosUso.AdministrarVentas.RegistrarVenta;

public  class RegistrarVentaHandler
{
    private readonly IVentaRepository _ventaRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IMapper _mapper;

    public RegistrarVentaHandler(IVentaRepository ventaRepository, IProductoRepository productoRepository, IMapper mapper)
    {
        _ventaRepository = ventaRepository;
        _productoRepository = productoRepository;
        _mapper = mapper;
    }

    public async Task<RegistrarVentaResponse> Registrar(RegistrarVentaRequest request)
    {
        var response = new RegistrarVentaResponse();

        //Aplicando el automapper para convertir el objeto Request a venta dominio
        var venta = _mapper.Map<Models.Venta>(request);

        ///============Condiciones de validaciones


        foreach(var detalle in venta.Detalle)
        {
            //1 - Validar si el productos existe
            var productoEncontrado = await _productoRepository.ConsultarPorId(detalle.IdProducto);
            if(productoEncontrado == null)
            {
                throw new Exception($"Producto no encontrado, código {detalle.IdProducto}");
            }
            
            // Validar si existe stock suficiente
            //2 - Validar si existe stock suficiente - TODO
            if (productoEncontrado.Stock < detalle.Cantidad)
            {
                throw new Exception($"Stock insuficiente para el producto con ID {detalle.IdProducto}");
            }
            
            //3 - Reservar el stock del producto - TODO
            //3.1 --Si sucedio algun erro al reservar el producto , retornar una exepcion
            
            // Reservar el stock del producto
            try
            {
                await _ventaRepository.ReservarStock(detalle.IdProducto, detalle.Cantidad);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al reservar el stock del producto con ID {detalle.IdProducto}", ex);
            }
        }

        // SI todo esta OK
        
        try
        {
            await _ventaRepository.Registrar(venta);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al registrar la venta con el cliente { venta.Cliente }", ex);
        }
        

        return response;
    }

}