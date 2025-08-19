using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTwo.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class relacaotabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Profile",
                table: "Members",
                newName: "ProfileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ProfileTypeId",
                table: "Members",
                column: "ProfileTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_ProfileTypes_ProfileTypeId",
                table: "Members",
                column: "ProfileTypeId",
                principalTable: "ProfileTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_ProfileTypes_ProfileTypeId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_ProfileTypeId",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "ProfileTypeId",
                table: "Members",
                newName: "Profile");
        }
    }
}
