using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Venta.Domain.Repositories;
using Ventas.Application.Common;

namespace Ventas.Application.CasosUso.AdministrarProductos.ConsultarProductos
{
    public class ConsultarProductosHandler : IRequestHandler<ConsultarProductosRequest, IResult>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public ConsultarProductosHandler(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(ConsultarProductosRequest request, CancellationToken cancellationToken)
        {

            IResult response = null;

            try
            {
               
                var datos = await _productoRepository.Consultar(request.FiltroPorNombre);
                response = new SuccessResult<IEnumerable<ConsultaProducto>>(
                    _mapper.Map<IEnumerable<ConsultaProducto>>(datos)
                );

                return response;
            }
            catch(Exception ex)
            {
                response = new FailureResult();
                return response;
            }
        }
    }
}
