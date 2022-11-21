using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogueWebApi.Migrations
{
    /// <inheritdoc />
    public partial class CategoryidchangedtoCatedoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Items",
                newName: "CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Items",
                newName: "CategoryId");
        }
    }
}
