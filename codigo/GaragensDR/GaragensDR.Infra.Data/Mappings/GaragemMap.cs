using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GaragensDR.Infra.Data.Mappings
{
    public class GaragemMap : BaseMap<Garagem>
    {
        public override void Configure(EntityTypeBuilder<Garagem> builder)
        {
            builder.ToTable("Garagens");

            builder.Property(c => c.Ativo)
                 .IsRequired()
                 .HasColumnName("Ativo")
                 .HasDefaultValue(true);

            builder.Property(c => c.DataCriacao)
                .IsRequired()
                .HasColumnName("DataCriacao")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            builder.Property(c => c.DataAtualizacao)
                .HasColumnName("DataAtualizacao")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");
        }
    }
}
