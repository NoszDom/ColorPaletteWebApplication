using Microsoft.EntityFrameworkCore.Migrations;

namespace ColorPaletteApp.Infrastructure.Migrations
{
    public partial class NavigationProperties4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorPalette_User_UserId",
                table: "ColorPalette");

            migrationBuilder.DropForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteId",
                table: "Save");

            migrationBuilder.DropForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteID",
                table: "Save");

            migrationBuilder.DropForeignKey(
                name: "FK_Save_User_UserId",
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
                name: "IX_ColorPalette_UserId",
                table: "ColorPalette");

            migrationBuilder.DropColumn(
                name: "ColorPaletteID",
                table: "Save");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Save");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ColorPalette");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Save",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "ColorPaletteId",
                table: "Save",
                newName: "ColorPaletteID");

            migrationBuilder.RenameIndex(
                name: "IX_Save_UserId",
                table: "Save",
                newName: "IX_Save_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Save_ColorPaletteId",
                table: "Save",
                newName: "IX_Save_ColorPaletteID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Save",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ColorPaletteID",
                table: "Save",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteID",
                table: "Save");

            migrationBuilder.DropForeignKey(
                name: "FK_Save_User_UserID",
                table: "Save");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Save",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ColorPaletteID",
                table: "Save",
                newName: "ColorPaletteId");

            migrationBuilder.RenameIndex(
                name: "IX_Save_UserID",
                table: "Save",
                newName: "IX_Save_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Save_ColorPaletteID",
                table: "Save",
                newName: "IX_Save_ColorPaletteId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Save",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ColorPaletteId",
                table: "Save",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ColorPaletteID",
                table: "Save",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Save",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ColorPalette",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Save_ColorPaletteID",
                table: "Save",
                column: "ColorPaletteID");

            migrationBuilder.CreateIndex(
                name: "IX_Save_UserID",
                table: "Save",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ColorPalette_UserId",
                table: "ColorPalette",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorPalette_User_UserId",
                table: "ColorPalette",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteId",
                table: "Save",
                column: "ColorPaletteId",
                principalTable: "ColorPalette",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Save_ColorPalette_ColorPaletteID",
                table: "Save",
                column: "ColorPaletteID",
                principalTable: "ColorPalette",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Save_User_UserId",
                table: "Save",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Save_User_UserID",
                table: "Save",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
