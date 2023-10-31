using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Algoritmo.CharlaEFC.Domain.Jerarquias.Enum;
using Algoritmo.Microservices.Shared.Infrastructure.DataBases.Commnon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Algoritmo.CharlaEFC.Infrastructure.EntityConfigurations
{
    public class JerarquiaItemEntityConfiguration : IEntityTypeConfiguration<JerarquiaItem>
    {
        public void Configure(EntityTypeBuilder<JerarquiaItem> builder)
        {

            builder.HasKey(j => j.Id);
            builder.Property(j => j.Id).ValueGeneratedOnAdd();
            builder.Property(j => j.Codigo).HasMaxLength(9);
            builder.Property(j => j.Nombre).HasMaxLength(150);
            builder.Ignore(j => j.Entidad);
            builder.Ignore(j => j.EntidadInformacion);
            builder.Ignore(j => j.Nivel);
            builder.Ignore(j => j.NombreConcatenado);
            builder.Ignore(j => j.CodigoConcatenado);

            builder.HasOne(jh => jh.Padre)
                .WithMany(jp => jp.Hijos)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            builder.HasOne(ji => ji.Jerarquia)
                .WithMany(j=>j.Arbol)
                .OnDelete(DeleteBehavior.Cascade);

            builder.CreateIndex(i => i.Codigo, i => i.Jerarquia).IsUnique(true)
                    .HasFilter($"[{nameof(JerarquiaItem.Tipo)}] != {TipoJerarquiaItem.Hoja.Value}");
            builder.CreateIndex(i => i.Jerarquia, i => i.EntidadMaestraId, i => i.TipoEntidadFullName).IsUnique(true)
                    .HasFilter($"[{nameof(JerarquiaItem.Tipo)}] = {TipoJerarquiaItem.Hoja.Value}");
        }
    }
}