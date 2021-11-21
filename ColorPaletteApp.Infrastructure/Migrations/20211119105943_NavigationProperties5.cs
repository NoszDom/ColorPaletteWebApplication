using Microsoft.EntityFrameworkCore.Migrations;

namespace ColorPaletteApp.Infrastructure.Migrations
{
    public partial class NavigationProperties5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteID",
                table: "Save");

            migrationBuilder.DropForeignKey(
                name: "FK_Save_User_UserID",
                table: "Save");

            migrationBuilder.AddForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteID",
                table: "Save",
                column: "ColorPaletteID",
                principalTable: "ColorPalette",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Save_User_UserID",
                table: "Save",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteID",
                table: "Save");

            migrationBuilder.DropForeignKey(
                name: "FK_Save_User_UserID",
                table: "Save");

            migrationBuilder.AddForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteID",
                table: "Save",
                column: "ColorPaletteID",
                principalTable: "ColorPalette",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Save_User_UserID",
                table: "Save",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
