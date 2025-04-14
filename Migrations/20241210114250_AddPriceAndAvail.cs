using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zafaty.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceAndAvail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "available",
                table: "Post",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "Post",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "available",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Post");
        }
    }
}
