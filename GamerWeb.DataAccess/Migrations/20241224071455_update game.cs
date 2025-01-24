using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamerWeb.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updategame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReleaseDate",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
