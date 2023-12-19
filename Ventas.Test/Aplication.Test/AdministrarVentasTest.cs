using AutoMapper;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Venta.Domain.Models;
using Venta.Domain.Repositories;
using Ventas.Application.CasosUso.AdministrarProductos.ConsultarProductos;
using Ventas.Application.CasosUso.AdministrarVentas.RegistrarVenta;

namespace Ventas.Test.Aplication.Test;

public class AdministrarVentasTest
{
    
    private readonly IVentaRepository _ventaRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IMapper _mapper;
    private readonly RegistrarVentaHandler _registrarVentaHandler;
    public AdministrarVentasTest()
    {
        _mapper = new MapperConfiguration(c => c.AddProfile<RegistrarVentaMapper>()).CreateMapper();

        _productoRepository = Substitute.For<IProductoRepository>();
        _ventaRepository = Substitute.For<IVentaRepository>();
        _registrarVentaHandler = Substitute.For<RegistrarVentaHandler>(_ventaRepository,_productoRepository, _mapper);
        
       // _registrarVentaHandler = new RegistrarVentaHandler (_productoRepository, _ventaRepository, _mapper);
        
    }
    
    [Fact]
    public async Task ProductoNoExiste()
    {
   
        _productoRepository.ConsultarPorId(Arg.Any<int>()).Returns(Task.FromResult<Producto>(null));
        var reqVenta = RegistrarVentaRequest();
 
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await _registrarVentaHandler.Registrar(reqVenta);
        });
    }
    [Fact]
    public async Task StockInsuficiente()
    {
 
        _productoRepository.ConsultarPorId(Arg.Any<int>()).Returns(Task.FromResult(new Producto { Stock = 5 }));
        _ventaRepository.ReservarStock(Arg.Any<int>(), Arg.Any<int>()).Throws(new Exception());

        var reqVenta = RegistrarVentaRequest();
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await _registrarVentaHandler.Registrar(reqVenta);
        });
    }
    [Fact]
    public async Task RegistrarVenta()
    {
  
        _productoRepository.ConsultarPorId(Arg.Any<int>()).Returns(Task.FromResult(new Producto { Stock = 10 }));

        var reqVenta = RegistrarVentaRequest();
        var result = await _registrarVentaHandler.Registrar( reqVenta );

 
        Assert.NotNull(result); 
    }
 

    private static RegistrarVentaRequest RegistrarVentaRequest()
    {
        var reqDetalle = new List<RegistrarVentaDetalleRequest> {
            new RegistrarVentaDetalleRequest{ IdProducto = 1, Cantidad = 5, Precio = 120 },
            new RegistrarVentaDetalleRequest{ IdProducto = 2, Cantidad = 3, Precio = 1300 } ,
            new RegistrarVentaDetalleRequest{ IdProducto = 3, Cantidad = 1, Precio = 43 } ,
            new RegistrarVentaDetalleRequest{ IdProducto = 4, Cantidad = 0, Precio = 5678 } 
        };

        var reqVenta = new RegistrarVentaRequest() { IdCliente = 1 , Productos = reqDetalle};
        return reqVenta;
    }
}