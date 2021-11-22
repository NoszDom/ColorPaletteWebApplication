using Microsoft.EntityFrameworkCore.Migrations;

namespace ColorPaletteApp.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ColorPalette",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Colors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorPalette", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColorPalette_User_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Save",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ColorPaletteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Save", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Save_ColorPalette_ColorPaletteID",
                        column: x => x.ColorPaletteID,
                        principalTable: "ColorPalette",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Save_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorPalette_CreatorID",
                table: "ColorPalette",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Save_ColorPaletteID",
                table: "Save",
                column: "ColorPaletteId");

            migrationBuilder.CreateIndex(
                name: "IX_Save_UserID",
                table: "Save",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Save");

            migrationBuilder.DropTable(
                name: "ColorPalette");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
