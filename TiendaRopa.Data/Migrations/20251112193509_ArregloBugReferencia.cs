using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaRopa.Data.Migrations
{
    /// <inheritdoc />
    public partial class ArregloBugReferencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturaCompras_Factura_IdFactura",
                table: "FacturaCompras");

            migrationBuilder.DropIndex(
                name: "IX_FacturaCompras_IdFactura",
                table: "FacturaCompras");

            migrationBuilder.AddColumn<int>(
                name: "FacturaIdFactura",
                table: "FacturaCompras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacturaCompras_FacturaIdFactura",
                table: "FacturaCompras",
                column: "FacturaIdFactura");

            migrationBuilder.AddForeignKey(
                name: "FK_FacturaCompras_Factura_FacturaIdFactura",
                table: "FacturaCompras",
                column: "FacturaIdFactura",
                principalTable: "Factura",
                principalColumn: "IdFactura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturaCompras_Factura_FacturaIdFactura",
                table: "FacturaCompras");

            migrationBuilder.DropIndex(
                name: "IX_FacturaCompras_FacturaIdFactura",
                table: "FacturaCompras");

            migrationBuilder.DropColumn(
                name: "FacturaIdFactura",
                table: "FacturaCompras");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaCompras_IdFactura",
                table: "FacturaCompras",
                column: "IdFactura");

            migrationBuilder.AddForeignKey(
                name: "FK_FacturaCompras_Factura_IdFactura",
                table: "FacturaCompras",
                column: "IdFactura",
                principalTable: "Factura",
                principalColumn: "IdFactura",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
