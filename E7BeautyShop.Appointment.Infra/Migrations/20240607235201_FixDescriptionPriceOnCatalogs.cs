using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E7BeautyShop.Appointment.Infra.Migrations
{
    /// <inheritdoc />
    public partial class FixDescriptionPriceOnCatalogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescriptionPrime",
                table: "Catalogs",
                newName: "DescriptionPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescriptionPrice",
                table: "Catalogs",
                newName: "DescriptionPrime");
        }
    }
}
