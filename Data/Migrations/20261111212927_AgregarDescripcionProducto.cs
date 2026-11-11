using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDescripcionProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Products",
                newName: "Inventory");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Inventory",
                table: "Products",
                newName: "Stock");
        }
    }
}
