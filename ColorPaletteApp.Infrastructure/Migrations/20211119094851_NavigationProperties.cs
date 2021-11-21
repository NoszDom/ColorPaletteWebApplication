using Microsoft.EntityFrameworkCore.Migrations;

namespace ColorPaletteApp.Infrastructure.Migrations
{
    public partial class NavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Save",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ColorPalette",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Save_ColorPaletteID",
                table: "Save",
                column: "ColorPaletteID");

            migrationBuilder.CreateIndex(
                name: "IX_Save_UserID",
                table: "Save",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ColorPalette_CreatorID",
                table: "ColorPalette",
                column: "CreatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorPalette_User_CreatorID",
                table: "ColorPalette",
                column: "CreatorID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteID",
                table: "Save",
                column: "ColorPaletteID",
                principalTable: "ColorPalette",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Save_User_UserID",
                table: "Save",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorPalette_User_CreatorID",
                table: "ColorPalette");

            migrationBuilder.DropForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteID",
                table: "Save");

            migrationBuilder.DropForeignKey(
                name: "FK_Save_User_UserID",
                table: "Save");

            migrationBuilder.DropIndex(
                name: "IX_Save_ColorPaletteID",
                table: "Save");

            migrationBuilder.DropIndex(
                name: "IX_Save_UserID",
                table: "Save");

            migrationBuilder.DropIndex(
                name: "IX_ColorPalette_CreatorID",
                table: "ColorPalette");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Save",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ColorPalette",
                newName: "ID");
        }
    }
}
