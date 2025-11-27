using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaRopa.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPrecioRopa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Precio",
                table: "Ropa",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Tela",
                table: "Ropa",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Ropa");

            migrationBuilder.DropColumn(
                name: "Tela",
                table: "Ropa");
        }
    }
}
