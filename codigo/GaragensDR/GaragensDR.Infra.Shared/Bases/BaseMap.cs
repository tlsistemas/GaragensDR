using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaragensDR.Infra.Shared.Bases
{
    public class BaseMap<T> : IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnName("Id");

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
