using Algoritmo.CharlaEFC.Domain.Jerarquias.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Algoritmo.Microservices.Shared.Infrastructure.DataBases.Commnon;

namespace Algoritmo.CharlaEFC.Infrastructure.EntityConfigurations
{

    public class JerarquiaNivelEntityConfiguration : IEntityTypeConfiguration<JerarquiaNivel>
    {
        public void Configure(EntityTypeBuilder<JerarquiaNivel> builder)
        {
            builder.HasKey(j => j.Id);
            builder.Property(j => j.Id).ValueGeneratedOnAdd();
            builder.Property(j => j.Nivel).HasPrecision(3).IsRequired();

            builder.HasOne(jn => jn.Jerarquia)
                   .WithMany(j => j.Niveles)
                   .OnDelete(DeleteBehavior.Cascade);


            //builder.HasIndex("JerarquiaId", nameof(JerarquiaNivel.Nivel)).IsUnique();
            builder.CreateUniqueIndex(new string[] { "JerarquiaId", nameof(JerarquiaNivel.Nivel) });
        }
    }
}