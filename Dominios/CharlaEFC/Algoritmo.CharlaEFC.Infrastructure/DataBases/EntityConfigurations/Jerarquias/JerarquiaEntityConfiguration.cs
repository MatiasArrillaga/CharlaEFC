using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Algoritmo.CharlaEFC.Infrastructure.EntityConfigurations
{

    /// <remark>
    /// https://docs.microsoft.com/en-us/ef/core/modeling/inheritance
    /// </remark>
    public class JerarquiaEntityConfiguration : IEntityTypeConfiguration<Jerarquia>
    {
        public void Configure(EntityTypeBuilder<Jerarquia> builder)
        {
            builder.HasKey(j => j.Id);
            builder.Property(j => j.Id).ValueGeneratedOnAdd();
            builder.Property(j => j.Codigo).HasMaxLength(9).IsRequired();
            builder.Property(j => j.Nombre).HasMaxLength(150).IsRequired();
            builder.Property(j => j.Descripcion).HasMaxLength(150);
            builder.Property(j => j.Activo);
            builder.Property(j => j.PermiteHojasYRamasEnMismoNivel);
            builder.Property(j => j.TipoEntidadFullName).IsRequired();
            builder.Property(j => j.TipoEntidadAssembly).IsRequired();
            builder.Ignore(j => j.Root);
            builder.Ignore(j => j.Hojas);
            //builder.HasIndex(nameof(Jerarquia.Codigo)).IsUnique();
        }
    }
}