using AutoMapper;
using Models = Venta.Domain.Models;
namespace Ventas.Application.CasosUso.AdministrarVentas.RegistrarVenta;

public class RegistrarVentaMapper: Profile
{
    public RegistrarVentaMapper() {
        CreateMap<RegistrarVentaDetalleRequest, Models.VentaDetalle>();
        CreateMap<RegistrarVentaRequest, Models.Venta>()
            .ForMember(dest => dest.Detalle, map => map.MapFrom(src => src.Productos));
    }
    
}