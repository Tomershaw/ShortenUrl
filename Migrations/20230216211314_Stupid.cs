using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proUrl.Migrations
{
    /// <inheritdoc />
    public partial class Stupid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UrlPairs");

            migrationBuilder.AddColumn<string>(
                name: "UrlUserId",
                table: "UrlPairs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UrlPairs_UrlUserId",
                table: "UrlPairs",
                column: "UrlUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlPairs_AspNetUsers_UrlUserId",
                table: "UrlPairs",
                column: "UrlUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlPairs_AspNetUsers_UrlUserId",
                table: "UrlPairs");

            migrationBuilder.DropIndex(
                name: "IX_UrlPairs_UrlUserId",
                table: "UrlPairs");

            migrationBuilder.DropColumn(
                name: "UrlUserId",
                table: "UrlPairs");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UrlPairs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
