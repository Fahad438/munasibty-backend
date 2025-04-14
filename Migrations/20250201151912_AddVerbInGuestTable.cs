using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zafaty.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddVerbInGuestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInvited",
                table: "Guests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberGuest",
                table: "Guests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "Guests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvited",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "NumberGuest",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Guests");
        }
    }
}
