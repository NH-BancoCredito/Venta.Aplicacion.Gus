﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Domain.Models;

namespace Ventas.Application.CasosUso.AdministrarProductos.ConsultarProductos
{
    public class ConsultarProductosMapper : Profile
    {
        public ConsultarProductosMapper()
        {
            CreateMap<Producto, ConsultaProducto>();
        }
    }
}
