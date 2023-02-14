using Fadami.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fadami.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .ToTable("USUARIO");

            builder
                .HasKey(u => u.Codigo);

            builder.Property(u => u.Codigo)
                   .HasColumnName("CODIGO");

            builder
                .Property(u => u.Nome)
                .HasColumnName("NOME")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(u => u.Login)
                .HasColumnName("LOGIN")
                .HasColumnType("varchar(20)");

            builder
                .Property(u => u.CPF)
                .HasColumnName("CPF")
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder
                .Property(u => u.Senha)
                .HasColumnName("SENHA")
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder
                .Property(u => u.UltimoAcesso)
                .HasColumnName("ULTIMO_ACESSO")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            builder
                .Property(u => u.QtdErroLogin)
                .HasColumnName("QTD_ERRO_LOGIN")
                .HasColumnType("int");

            builder
                .Property(u => u.BLAtivo)
                .HasColumnName("BL_ATIVO");
        }
    }
}