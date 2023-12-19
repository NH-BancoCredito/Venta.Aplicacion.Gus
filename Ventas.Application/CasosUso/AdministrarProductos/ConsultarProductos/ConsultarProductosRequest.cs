using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ventas.Application.CasosUso.AdministrarProductos.ConsultarProductos
{
    public class ConsultarProductosRequest : IRequest<ConsultarProductosResponse>
    {
        public string FiltroPorNombre { get; set; }
    }
}
