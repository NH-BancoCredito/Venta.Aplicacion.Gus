using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Domain.Models;
using Venta.Domain.Repositories;
using Ventas.Application.CasosUso.AdministrarProductos.ConsultarProductos;

namespace Ventas.Test.Aplication.Test
{
    public class AdministrarProductosTests
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ConsultarProductosHandler _consultarProductosHandler;

        public AdministrarProductosTests()
        {
            _mapper = new MapperConfiguration(c => c.AddProfile<ConsultarProductosMapper>()).CreateMapper();

            _productoRepository = Substitute.For<IProductoRepository>();
            _consultarProductosHandler = Substitute.For<ConsultarProductosHandler>(_productoRepository, _mapper);
        }

 

        [Fact]
        public async Task ConsultarProductos()
        {
            var request = new ConsultarProductosRequest() { FiltroPorNombre = "producto" };
            
            var lsProductos = ListaProductos();
            _productoRepository.Consultar(default).ReturnsForAnyArgs(lsProductos);

            var response = await _consultarProductosHandler.Handle(request);

            Assert.True(response.Resultado.ToList().Count > 0);

        }

        private List<Producto> ListaProductos()
        {
            var lstProducto = new List<Producto>();
            var producto1 = new Producto() { IdProducto = 1, Nombre = "Chocolate" };
            var producto2 = new Producto() { IdProducto = 2, Nombre = "Galletas" };
            var producto3 = new Producto() { IdProducto = 3, Nombre = "Agua" };
       
            lstProducto.Add(producto1);
            lstProducto.Add(producto2);
            lstProducto.Add(producto3);
            return lstProducto;
        }
       
    }

}
