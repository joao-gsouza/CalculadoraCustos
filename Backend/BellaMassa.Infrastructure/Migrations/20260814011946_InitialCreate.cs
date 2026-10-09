using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BellaMassa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecoEmbalagem = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    QuantidadeEmbalagem = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItensFicaTecnica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PratoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoBaseId = table.Column<int>(type: "int", nullable: false),
                    QuantidadeUtilizada = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensFicaTecnica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensFicaTecnica_Pratos_PratoId",
                        column: x => x.PratoId,
                        principalTable: "Pratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensFicaTecnica_ProdutosBase_ProdutoBaseId",
                        column: x => x.ProdutoBaseId,
                        principalTable: "ProdutosBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensFicaTecnica_PratoId",
                table: "ItensFicaTecnica",
                column: "PratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensFicaTecnica_ProdutoBaseId",
                table: "ItensFicaTecnica",
                column: "ProdutoBaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensFicaTecnica");

            migrationBuilder.DropTable(
                name: "Pratos");

            migrationBuilder.DropTable(
                name: "ProdutosBase");
        }
    }
}
