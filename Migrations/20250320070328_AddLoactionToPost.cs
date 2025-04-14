using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zafaty.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddLoactionToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Post",
                type: "text",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Post");
        }
    }
}
