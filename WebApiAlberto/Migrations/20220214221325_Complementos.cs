using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAlberto.Migrations
{
    public partial class Complementos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Complementos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComputadorasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complementos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complementos_Computadoras_ComputadorasId",
                        column: x => x.ComputadorasId,
                        principalTable: "Computadoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complementos_ComputadorasId",
                table: "Complementos",
                column: "ComputadorasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complementos");
        }
    }
}
