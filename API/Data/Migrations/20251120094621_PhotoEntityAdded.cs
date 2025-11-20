using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class PhotoEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Members_MenberId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "MenberId",
                table: "Photos",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_MenberId",
                table: "Photos",
                newName: "IX_Photos_MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Members_MemberId",
                table: "Photos",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Members_MemberId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Photos",
                newName: "MenberId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_MemberId",
                table: "Photos",
                newName: "IX_Photos_MenberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Members_MenberId",
                table: "Photos",
                column: "MenberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
