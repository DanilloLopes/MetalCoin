using System;
using Metalcoin.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Metalcoin.Core.Enums;

namespace MetalCoin.Infra.Data.Mappings
{
    internal class CupomMapping : IEntityTypeConfiguration<Cupom>
    {
        public void Configure(EntityTypeBuilder<Cupom> builder)
        {
            builder.ToTable("Cupons");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Codigo)
                .HasColumnType("varchar(15)")
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(25)")
                .IsRequired();

            builder.Property(p => p.ValorDesconto)
                .HasColumnType("decimal")
                .IsRequired();

            builder.Property(p => p.TipoDesconto)
                .HasColumnType("int")
                .HasConversion(
                    v => (int)v,
                    v => (TipoDesconto)v)
                .IsRequired();

            builder.Property(p => p.DataValidade)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.QuantidadeLiberada)
                .IsRequired();

            builder.Property(p => p.QuantidadeUsada)
                .IsRequired();

            builder.Property(p => p.Status)
                .HasColumnType("int")
                .HasConversion(
                    v => (int)v,
                    v => (TipoStatus)v)
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.DataAlteracao)
                .IsRequired()
                .HasColumnType("datetime");

        }
    }
}
