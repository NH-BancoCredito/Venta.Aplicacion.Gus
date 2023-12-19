using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Venta.Domain.Repositories;

namespace Ventas.Application.CasosUso.AdministrarProductos.ConsultarProductos
{
    public class ConsultarProductosHandler : IRequestHandler<ConsultarProductosRequest, ConsultarProductosResponse>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public ConsultarProductosHandler(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<ConsultarProductosResponse> Handle(ConsultarProductosRequest request)
        {
            var response = new ConsultarProductosResponse();

            var datos = await _productoRepository.Consultar(request.FiltroPorNombre);

            response.Resultado = _mapper.Map<IEnumerable<ConsultaProducto>>(datos);


            return response;
        }

        public async Task<ConsultarProductosResponse> Handle(ConsultarProductosRequest request, CancellationToken cancellationToken)
        {
            var response = new ConsultarProductosResponse();

            var datos = await _productoRepository.Consultar(request.FiltroPorNombre);

            response.Resultado = _mapper.Map<IEnumerable<ConsultaProducto>>(datos);


            return response;
        }
    }
}
