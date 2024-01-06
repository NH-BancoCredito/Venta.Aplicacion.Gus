using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venta.Domain.Models;

namespace Venta.Infrastructure.Repositories.Base.EFConfigurations;

public class CategoriaEntityTypeConfiguration
    : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categoria");
        builder.HasKey(c => c.IdCategoria);
            
    }
}