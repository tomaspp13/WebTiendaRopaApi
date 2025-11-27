using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaRopa.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregadoDeDatosUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Usuarios",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "Contraseña",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contraseña",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Usuarios",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "Mail");
        }
    }
}
