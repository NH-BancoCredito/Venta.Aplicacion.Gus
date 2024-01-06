using AutoMapper;
using MediatR;
using Venta.Domain.Repositories;
using Ventas.Application.Common;
using Models = Venta.Domain.Models;


namespace Ventas.Application.CasosUso.AdministrarVentas.RegistrarVenta;

public  class RegistrarVentaHandler : IRequestHandler<RegistrarVentaRequest, IResult>
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

    public async Task<IResult> Registrar(RegistrarVentaRequest request)
    {
        IResult response = null;

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
                await _productoRepository.ReservarStock(detalle.IdProducto, detalle.Cantidad);
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

            response = new SuccessResult<int>(venta.IdVenta);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al registrar la venta con el cliente { venta.Cliente }", ex);
        }
        

        return response;
    }

    public async Task<IResult> Handle(RegistrarVentaRequest request, CancellationToken cancellationToken)
    {
        IResult response = null;

        //var response = new RegistrarVentaResponse();

        var validator = new RegistrarVentaValidator();
        var validationResult = validator.Validate(request);
        if(!validationResult.IsValid)
        {
            return new FailureResult<DetailError>(new DetailError("00'",validationResult.ToString("/")));

        }

        //Aplicando el automapper para convertir el objeto Request a venta dominio
        var venta = _mapper.Map<Models.Venta>(request);

        ///============Condiciones de validaciones


        foreach (var detalle in venta.Detalle)
        {
            //1 - Validar si el productos existe
            var productoEncontrado = await _productoRepository.ConsultarPorId(detalle.IdProducto);
            if (productoEncontrado?.IdProducto <= 0)
            {
                throw new Exception($"Producto no encontrado, código {detalle.IdProducto}");
            }



            //2 - Validar si existe stock suficiente - TODO

            //3 - Reservar el stock del producto - TODO
            //3.1 --Si sucedio algun erro al reservar el producto , retornar una exepcion
        }

        /// SI todo esta OK
        /// Registrar la venta - TODO
        /// 
        await _ventaRepository.Registrar(venta);

        response = new SuccessResult<int>(venta.IdVenta);

        return response;
    }
}