using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fadami.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LOGIN = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    CPF = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    SENHA = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ULTIMO_ACESSO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    QTD_ERRO_LOGIN = table.Column<int>(type: "int", nullable: false),
                    BL_ATIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.CODIGO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
