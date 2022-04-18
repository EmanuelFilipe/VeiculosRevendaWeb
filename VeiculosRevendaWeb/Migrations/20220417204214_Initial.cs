using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeiculosRevendaWeb.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CodStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proprietarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(18)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(9)", nullable: false),
                    CodStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Renavam = table.Column<string>(type: "nvarchar(11)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    AnoFabricacao = table.Column<DateTime>(nullable: true),
                    AnoModelo = table.Column<DateTime>(nullable: true),
                    Quilometragem = table.Column<string>(nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    CodStatus = table.Column<int>(nullable: false),
                    proprietarioId = table.Column<int>(nullable: true),
                    marcaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculos_Proprietarios_proprietarioId",
                        column: x => x.proprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_proprietarioId",
                table: "Veiculos",
                column: "proprietarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Proprietarios");
        }
    }
}
