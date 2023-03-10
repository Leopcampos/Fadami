// <auto-generated />
using System;
using Fadami.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fadami.Migrations
{
    [DbContext(typeof(SqlContext))]
    [Migration("20230214012151_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Fadami.Models.Usuario", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CODIGO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"), 1L, 1);

                    b.Property<bool>("BLAtivo")
                        .HasColumnType("bit")
                        .HasColumnName("BL_ATIVO");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)")
                        .HasColumnName("CPF");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("LOGIN");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOME");

                    b.Property<int>("QtdErroLogin")
                        .HasColumnType("int")
                        .HasColumnName("QTD_ERRO_LOGIN");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("SENHA");

                    b.Property<DateTime>("UltimoAcesso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("ULTIMO_ACESSO")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Codigo");

                    b.ToTable("USUARIO", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
